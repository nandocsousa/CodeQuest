using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button executeButton;
    [SerializeField] private TMP_Text feedbackText;

    private void Start()
    {
        executeButton.onClick.AddListener(ProcessInput);

        feedbackText.text = "Type a command: Play() or Quit()";
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
        string input = inputField.text.ToLower();

        if (input == "play()")
        {
            feedbackText.text = "Starting the game...";
            StartCoroutine(StartCountdown());
        }
        else if (input == "quit()")
        {
            feedbackText.text = "Quitting the game...";
            Application.Quit();
        }
        else feedbackText.text = "Invalid command. Try Play() or Quit()";

        inputField.text = ""; // Clear input field for the next command
        
        RefocusInputField();
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Level1");
    }

    private void RefocusInputField()
    {
        inputField.Select();
        inputField.ActivateInputField(); // Ensures input mode is active
    }
}