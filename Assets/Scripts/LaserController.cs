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
        // Cast the ray
        RaycastHit2D hit = Physics2D.Raycast(laserLine.transform.position, player.up, laserDistance, laserMask);

        // Update the LineRenderer positions
        laserLine.SetPosition(0, laserLine.transform.position);

        if (hit.collider != null)
        {
            laserLine.SetPosition(1, hit.point);
            //Debug.Log("Laser hit: " + hit.collider.name);
        }
        else laserLine.SetPosition(1, laserLine.transform.position * laserDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActivateLaser = true;
            Debug.Log("Laser can now be activated.");
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
}