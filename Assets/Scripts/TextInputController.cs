using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInputController : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button executeButton;

    public TMP_Text errorText; // Display error messages

    private Transform player;

    private float moveDistance = 1f; // Distance the player moves on the grid
    private int maxSteps = 9; // Maximum steps allowed at once

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        executeButton.onClick.AddListener(GetInputOnClick); // Link GetInputOnClick() to the button click

        errorText.text = ""; // Clear error text at the start
    }

    private void Update()
    {
        // Check for Enter key press
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GetInputOnClick();
        }
    }

    private void GetInputOnClick()
    {
        string input = inputField.text; // Get the input text

        Debug.Log("Log input: " + input);

        if (input.StartsWith("Move"))
        {
            MoveInput(input);
        }
        else if (input.StartsWith("Rotate"))
        {
            RotateInput(input);
        }
        else
        {
            ShowErrorMessage("Invalid command. Use MoveDirection(steps) or Rotate(angle).");
        }

        inputField.text = ""; // Clear the input field for the next input
    }

    private void MoveInput(string input)
    {
        try
        {
            // Extract direction
            string direction = input.Substring(4, input.IndexOf('(') - 4); // Extract the text between "Move" and "(" ---> 0123 4... (

            // Extract steps converted to integer (Parse)
            int steps = int.Parse(input.Substring(input.IndexOf('(') + 1, input.IndexOf(')') - input.IndexOf('(') - 1)); // Extract the text between "(" and ")" ---> ( 0... )

            // Check if steps exceed the maximum limit
            if (steps > maxSteps)
            {
                ShowErrorMessage("You cannot move more than " + maxSteps + " steps at a time.");
                return;
            }

            // Calculate movement based on direction
            Vector3 moveVector = Vector3.zero;
            switch (direction)
            {
                case "Right":
                    moveVector = Vector3.right * steps * moveDistance;
                    break;
                case "Left":
                    moveVector = Vector3.left * steps * moveDistance;
                    break;
                case "Up":
                    moveVector = Vector3.up * steps * moveDistance;
                    break;
                case "Down":
                    moveVector = Vector3.down * steps * moveDistance;
                    break;
                default:
                    ShowErrorMessage("Invalid direction. Use Up, Down, Left, or Right.");
                    return;
            }

            player.position += moveVector; // Move the player

            errorText.text = ""; // Clear error text after a successful command
        }
        catch // Happens if int.Parse() fails
        {
            ShowErrorMessage("Invalid command. Ensure it's in the format MoveDirection(steps).");
        }
    }

    void RotateInput(string input)
    {
        try
        {
            // Extract angle
            int angle = int.Parse(input.Substring(input.IndexOf('(') + 1, input.IndexOf(')') - input.IndexOf('(') - 1)); // Extract the text between "(" and ")" ---> ( 0... )

            // Check if the angle is a valid increment of 90
            if (angle % 90 != 0)
            {
                ShowErrorMessage("Rotation angle must be an increment of 90.");
                return;
            }

            // Apply rotation
            player.Rotate(Vector3.forward, -angle); // Negative angle rotates clockwise
            Debug.Log("Player rotated by " + angle + " degrees.");

            errorText.text = ""; // Clear error text after a successful command
        }
        catch
        {
            ShowErrorMessage("Invalid command. Ensure it's in the format Rotate(angle).");
        }
    }

    private void ShowErrorMessage(string message)
    {
        errorText.text = message;
        Debug.LogError(message); // Log the error to the console as well
    }
}