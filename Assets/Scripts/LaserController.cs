using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private Transform player;

    private InputController inputController;

    [Header("Laser References")]
    [SerializeField] private LineRenderer laserLine; // Visually represents the laser
    public LayerMask laserMask; // The layers the laser should interact with

    public bool canActivateLaser = false;
    private bool laserActive = false;

    private float laserDistance = 100f;
    private int maxBounces = 10;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        inputController = GetComponent<InputController>();

        laserLine.enabled = false;
        laserLine.startWidth = 0.1f;
        laserLine.endWidth = 0.1f;

        Material laserMaterial = laserLine.material;
        laserMaterial.SetColor("_EmissionColor", Color.red * 2f);
    }

    private void Update()
    {
        if (laserActive)
            UpdateLaser();
    }

    public void ProcessActivateCommand(string input, bool activate = true)
    {
        if (!canActivateLaser)
        {
            inputController.ShowErrorMessage("Cannot activate the laser yet.");
            return;
        }

        if (input.Contains("laser"))
            ToggleLaser(activate);
        else inputController.ShowErrorMessage("Invalid object to activate or deactivate.");
    }

    private void ToggleLaser(bool state)
    {
        laserActive = state;
        laserLine.enabled = state;

        Debug.Log(state ? "Laser activated." : "Laser deactivated.");
    }

    private void UpdateLaser()
    {
        Vector3 origin = laserLine.transform.position;
        Vector3 direction = player.up;

        List<Vector3> laserPoints = new List<Vector3> { origin }; // List of laser points for LineRenderer

        int bounces = 0;

        while (bounces < maxBounces)
        {
            // Cast the ray
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, laserDistance, laserMask);

            if (hit.collider != null)
            {
                laserPoints.Add(hit.point); // Add the hit point to the laser points list

                if (hit.collider.CompareTag("Mirror"))
                {
                    // Reflect the laser and keep going
                    direction = Vector2.Reflect(direction, hit.normal);
                    origin = hit.point; // Start next raycast from the hit point
                    bounces++;
                }
                else if (hit.collider.CompareTag("Button"))
                {
                    Debug.Log("Laser hit the button!");

                    ButtonLaser buttonLaserScript = hit.collider.GetComponent<ButtonLaser>();
                    buttonLaserScript.ActivateButton();

                    break; // Stop the laser after hitting the button
                }
                else break; // Hit a non-mirror object, stop the laser
            }
            else
            {
                // No collision, extend laser to max distance
                laserPoints.Add(origin + direction * laserDistance);
                break;
            }
        }

        // Update the LineRenderer with all the points
        laserLine.positionCount = laserPoints.Count;
        laserLine.SetPositions(laserPoints.ToArray());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateLaser = true;
            Debug.Log("Laser can now be activated.");
        }

        GetComponent<SpriteRenderer>().enabled = false;
    }
}