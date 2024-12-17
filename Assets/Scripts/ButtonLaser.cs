using UnityEngine;

public class ButtonLaser : MonoBehaviour
{
    [SerializeField] private GameObject buttonOn;
    [SerializeField] private GameObject buttonOff;

    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;

    [SerializeField] private GameObject glass;

    private void Start()
    {
        buttonOn.SetActive(false);
        buttonOff.SetActive(true);

        lightOn.SetActive(false);
        lightOff.SetActive(true);

        glass.SetActive(true);
    }

    public void ActivateButton()
    {
        buttonOn.SetActive(true);
        buttonOff.SetActive(false);

        lightOn.SetActive(true);
        lightOff.SetActive(false);

        glass.SetActive(false);
        Debug.Log("Glass deactivated.");
    }
}