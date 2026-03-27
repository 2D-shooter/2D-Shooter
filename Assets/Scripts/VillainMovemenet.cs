using UnityEngine;

public class VillainMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float acceleration = 8f;
    public float changeTime = 2f;
    public float stoppingDistance = 0.8f;
    public float searchDuration = 3f;
    public float memoryDuration = 2f;

    private Transform target;
    private Vector2 direction;
    private float timer;
    private bool seesTarget;
    private float currentSpeed;
    private Vector2 lastKnownPlayerPos;
    private bool isSearching;
    private float searchTimer;
    private float lastSeenTime;

    private Rigidbody2D rb;
    private EnemyShooting enemyShooting;
    private Animator anim;

    bool isIdle;
    float idleTimer;
    public float idleTime = 2f;

    float collisionCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShooting = GetComponent<EnemyShooting>();
        anim = GetComponentInChildren<Animator>();

        currentSpeed = walkSpeed;
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        if (collisionCooldown > 0)
        {
            collisionCooldown -= Time.fixedDeltaTime;
            currentSpeed = 0f;
            rb.linearVelocity = Vector2.zero;
            anim?.SetBool("bool_run", false);
            return;
        }

        Vector2 moveDir = Vector2.zero;

        // === CHASE / MEMORY ===
        if ((seesTarget || (Time.time - lastSeenTime < memoryDuration)) && target != null)
        {
            Vector2 targetPos = (Vector2)target.position;
            float distance = Vector2.Distance(rb.position, targetPos);

            if (distance > stoppingDistance)
            {
                moveDir = (targetPos - rb.position).normalized;
                currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                currentSpeed = 0f;
                rb.linearVelocity = Vector2.zero;
                anim?.SetBool("bool_run", false);
                return;
            }
        }
        // === SEARCH LAST KNOWN POSITION ===
        else if (isSearching)
        {
            searchTimer -= Time.fixedDeltaTime;
            Vector2 dir = (lastKnownPlayerPos - rb.position).normalized;
            rb.linearVelocity = dir * walkSpeed * 0.5f;

            if (Vector2.Distance(rb.position, lastKnownPlayerPos) < 0.3f || searchTimer <= 0)
            {
                isSearching = false;
                rb.linearVelocity = Vector2.zero;
                ChooseNewDirection();
            }
        }
        // === PATROL ===
        else
        {
            if (isIdle)
            {
                idleTimer -= Time.fixedDeltaTime;
                currentSpeed = 0f;
                moveDir = Vector2.zero;

                if (idleTimer <= 0)
                {
                    isIdle = false;
                    ChooseNewDirection();
                }
            }
            else
            {
                timer -= Time.fixedDeltaTime;
                moveDir = direction;
                currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, acceleration * Time.fixedDeltaTime);

                if (timer <= 0)
                {
                    isIdle = true;
                    idleTimer = idleTime;
                }
            }
        }

        // === ROTATE ===
        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }

        rb.linearVelocity = moveDir * currentSpeed;
        anim?.SetBool("bool_run", currentSpeed > 0.1f);
    }

    void ChooseNewDirection()
    {
        timer = changeTime;
        Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        direction = dirs[Random.Range(0, dirs.Length)];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall")) return;

        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;
        rb.position += normal * 0.1f;
        currentSpeed = 0f;
        rb.linearVelocity = Vector2.zero;
        collisionCooldown = 0.2f;
        anim?.SetBool("bool_run", false);
    }

    // === PLAYER SPOTTED / LOST ===
    public void OnPlayerSpotted(Transform player)
    {
        seesTarget = true;
        target = player;
        lastSeenTime = Time.time;
        lastKnownPlayerPos = player.position;
        isSearching = false;
        enemyShooting?.SetTarget(player);
    }

    public void OnPlayerLost()
    {
        seesTarget = false;
        if (!isSearching)
        {
            isSearching = true;
            searchTimer = searchDuration;
        }
        enemyShooting?.ClearTarget();
    }

    // === DIRECT TARGET SET / CLEAR ===
    public void SetTarget(Transform t) => OnPlayerSpotted(t);
    public void ClearTarget() => OnPlayerLost();
}