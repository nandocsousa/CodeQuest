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
        string input = inputField.text.ToLower(); // Get input and convert to lowercase

        if (input.StartsWith("move"))
        {
            playerMovementController?.ProcessMoveCommand(input);
        }
        else if (input.StartsWith("rotate"))
        {
            playerMovementController?.ProcessRotateCommand(input);
        }
        else if (input.StartsWith("activate("))
        {
            laserController?.ProcessActivateCommand(input);
        }
        else if (input.StartsWith("deactivate("))
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