using UnityEngine;

public class VisionTriggerVillain : MonoBehaviour
{
    [Header("Masks")]
    public LayerMask obstacleMask; // seinät / esteet
    public LayerMask playerMask;   // pelaaja

    private VillainMovement villain;
    private Transform playerInRange;

    void Awake()
    {
        villain = GetComponentInParent<VillainMovement>();
        if (villain == null)
        {
            Debug.LogError("VillainMovement not found in parent!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.transform;
            villain?.OnPlayerSpotted(playerInRange);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
            villain?.OnPlayerLost();
        }
    }

    void FixedUpdate()
    {
        if (playerInRange == null || villain == null) return;

        Vector2 origin = villain.transform.position;
        Vector2 dir = (Vector2)playerInRange.position - origin;
        float dist = dir.magnitude;

        // Raycast tarkistaa, onko esteitä välissä
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, dist, obstacleMask | playerMask);
        Debug.DrawRay(origin, dir.normalized * dist, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            villain.OnPlayerSpotted(playerInRange);
        }
        else
        {
            villain.OnPlayerLost();
        }
    }
}