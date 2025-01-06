using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{
    [Header("Input UI")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button executeButton;
    [SerializeField] public TMP_Text errorText;

    [Header("References")]
    public LaserController laserController;
    private PlayerMovementController playerMovementController;
    private BoxController[] boxControllers;

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        boxControllers = new BoxController[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            boxControllers[i] = boxes[i].GetComponent<BoxController>();
        }
    }

    private void Start()
    {
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
        else if (input.StartsWith("push()"))
        {
            foreach (BoxController box in boxControllers)
            {
                box.ProcessPushCommand();
            }
        }

        inputField.text = ""; // Clear input field

        RefocusInputField();
    }

    public void ShowErrorMessage(string message)
    {
        errorText.text = message;
        Debug.LogError(message);
    }

    private void RefocusInputField()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}