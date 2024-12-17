using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    public float countdownTime = 90f;
    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    private void Update()
    {
        // Countdown logic
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Enable()
    {
        this.enabled = true;
    }
}