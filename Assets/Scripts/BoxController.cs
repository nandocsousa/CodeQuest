using UnityEngine;

public class BoxController : MonoBehaviour
{
    private InputController inputController;

    private GameObject player;
    public RayCastController rayCastController;

    public LayerMask boxMask;

    private void Awake()
    {
        inputController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputController>();
        player = GameObject.FindGameObjectWithTag("Player");
        rayCastController = player.GetComponent<RayCastController>();
    }

    public void ProcessPushCommand()
    {
        if (rayCastController.IsInFront(boxMask))
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 boxPosition = transform.position;

            Vector2 pushDirection = (boxPosition - playerPosition).normalized;
            PushBox(pushDirection);
        }
        else inputController.ShowErrorMessage("Are you trying to push air?");
    }

    public void PushBox(Vector2 pushDirection)
    {
        Vector2 newPos = (Vector2)transform.position + pushDirection;
        transform.position = newPos;
    }
}