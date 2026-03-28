using UnityEngine;

public class VisionTriggerVillain : MonoBehaviour
{
    [Header("Masks")]
    public LayerMask obstacleMask; // seinät
    public LayerMask playerMask;   // pelaaja

    private VillainMovement villain;
    private Transform playerInRange;

    private bool canSeePlayer = false;

    void Awake()
    {
        villain = GetComponentInParent<VillainMovement>();

        if (villain == null)
            Debug.LogError("VillainMovement not found in parent!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
            if (canSeePlayer)
            {
                canSeePlayer = false;
                villain?.OnPlayerLost();
            }
        }
    }

    void FixedUpdate()
    {
        if (playerInRange == null || villain == null) return;

        Vector2 origin = transform.position;
        Vector2 direction = (Vector2)playerInRange.position - origin;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, distance, obstacleMask | playerMask);
        Debug.DrawRay(origin, direction.normalized * distance, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (!canSeePlayer)
            {
                canSeePlayer = true;
                villain.OnPlayerSpotted(playerInRange);
            }
        }
        else
        {
            if (canSeePlayer)
            {
                canSeePlayer = false;
                villain.OnPlayerLost();
            }
        }
    }
}