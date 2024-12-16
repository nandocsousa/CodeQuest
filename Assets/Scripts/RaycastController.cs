using UnityEngine;

public class RayCastController : MonoBehaviour
{
    public LayerMask blockageLayer; // Layer for detecting blockage
    private float rayDistance = 1f;

    public bool IsBlockedInFront()
    {
        // Cast a ray in front of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, blockageLayer);

        Debug.DrawRay(transform.position, transform.up * rayDistance, Color.red); // Debug the ray for visualization

        return hit.collider != null; // Return true if the ray hits anything on the blackage layer
    }
}