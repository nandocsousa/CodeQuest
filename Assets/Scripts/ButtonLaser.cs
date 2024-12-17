using UnityEngine;

public class ButtonLaser : MonoBehaviour
{
    [SerializeField] private GameObject buttonOn;
    [SerializeField] private GameObject buttonOff;

    [SerializeField] private GameObject glass;

    private void Start()
    {
        buttonOn.SetActive(false);
        buttonOff.SetActive(true);

        glass.SetActive(true);
    }

    public void ActivateButton()
    {
        buttonOn.SetActive(true);
        buttonOff.SetActive(false);

        glass.SetActive(false);
        Debug.Log("Glass deactivated.");
    }
}