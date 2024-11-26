using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInputController : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button executeButton;

    private Transform player;

    private float moveDistance = 1f; // Distance the player moves on the grid

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // Link GetInputOnClick() to the button click
        executeButton.onClick.AddListener(GetInputOnClick);
    }

    void GetInputOnClick()
    {
        string input = inputField.text; // Get the input text

        Debug.Log("Log input: " + input);

        if (input.StartsWith("Move"))
        {
            MoveInput(input);
        }
        else
        {
            Debug.Log("Invalid command. Use MoveRight(steps), MoveLeft(steps), etc.");
        }

        inputField.text = ""; // Clear the input field for the next input
    }

    void MoveInput(string input)
    {
        try
        {
            // Extract direction
            string direction = input.Substring(4, input.IndexOf('(') - 4); // Extract the text between "Move" and "(" ---> 0123 4... (

            // Extract steps converted to integer (Parse)
            int steps = int.Parse(input.Substring(input.IndexOf('(') + 1, input.IndexOf(')') - input.IndexOf('(') - 1)); // Extract the text between "(" and ")" ---> ( 0... )

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
                    Debug.Log("Invalid direction. Use Up, Down, Left, or Right.");
                    return;
            }

            // Move the player
            player.position += moveVector;
        }
        catch // Happens if int.Parse() fails
        {
            Debug.Log("Failed to parse the command. Ensure it's in the format MoveDirection(steps).");
        }
    }
}