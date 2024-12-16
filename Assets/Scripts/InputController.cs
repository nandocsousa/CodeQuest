using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{
    [Header("Input UI")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button executeButton;
    [SerializeField] public TMP_Text errorText;

    private PlayerMovementController playerMovementController;
    [Header("Scripts References")]
    public LaserController laserController;

    private void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();

        executeButton.onClick.AddListener(ProcessInput);

        errorText.text = ""; // Clear error text
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            ProcessInput();

        if (!inputField.isFocused)
            RefocusInputField();
    }

    private void ProcessInput()
    {
        string input = inputField.text; // Get input text

        if (input.StartsWith("Move") || input.StartsWith("move") || input.StartsWith("MOVE"))
        {
            playerMovementController?.ProcessMoveCommand(input);
        }
        else if (input.StartsWith("Rotate") || input.StartsWith("rotate") || input.StartsWith("ROTATE"))
        {
            playerMovementController?.ProcessRotateCommand(input);
        }
        else if (input.StartsWith("Activate(") || input.StartsWith("activate(") || input.StartsWith("ACTIVATE("))
        {
            laserController?.ProcessActivateCommand(input);
        }
        else if (input.StartsWith("Deactivate(") || input.StartsWith("deactivate(") || input.StartsWith("DEACTIVATE("))
        {
            laserController?.ProcessActivateCommand(input, false);
        }

        inputField.text = ""; // Clear input field

        RefocusInputField();
    }

    public void ShowErrorMessage(string message)
    {
        errorText.text = message;
        Debug.LogError(message); // Log the error to the console as well
    }

    private void RefocusInputField()
    {
        inputField.Select();
        inputField.ActivateInputField(); // Ensures input mode is active
    }
}