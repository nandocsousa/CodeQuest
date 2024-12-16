using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Transform player;

    private float moveDistance = 1f; // Distance the player moves on the grid
    private int maxSteps = 9; // Maximum steps allowed at once

    private RayCastController rayCastController;
    private InputController inputController;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rayCastController = player.GetComponent<RayCastController>();

        inputController = GetComponent<InputController>();
    }

    public void ProcessMoveCommand(string input)
    {
        int steps = ExtractParameter(input);

        if (steps > maxSteps)
        {
            inputController.ShowErrorMessage("Cannot move more than " + maxSteps + " steps.");
            return;
        }
        else inputController.errorText.text = ""; // Clear error text

        // Check for blockage
        for (int i = 0; i < steps; i++)
        {
            if (rayCastController.IsBlockedInFront())
            {
                //inputController.ShowErrorMessage("Path in front has a blockage!");
                return; // Stop movement if there's a blockage
            }

            player.position += player.up * moveDistance; // Move the player
        }
    }

    public void ProcessRotateCommand(string input)
    {
        int angle = ExtractParameter(input);

        if (angle % 90 != 0)
        {
            inputController.ShowErrorMessage("Rotation angle must be an increment of 90.");
            return;
        }
        else inputController.errorText.text = ""; // Clear error text

        player.Rotate(Vector3.forward, -angle);
    }

    private int ExtractParameter(string input) // Extract the text between "(" and ")"
    {
        return int.Parse(input.Substring(input.IndexOf('(') + 1, input.IndexOf(')') - input.IndexOf('(') - 1));
    }
}