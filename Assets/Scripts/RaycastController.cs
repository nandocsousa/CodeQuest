using UnityEngine;

public class RayCastController : MonoBehaviour
{
    private float rayDistance = 1f;

    public bool IsInFront(LayerMask mask)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, mask);

        return hit.collider != null; // Return true if the ray hits anything on the layer
    }
}