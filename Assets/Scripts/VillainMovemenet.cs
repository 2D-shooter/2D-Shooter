using UnityEngine;

public class VillainMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float acceleration = 8f;
    public float changeTime = 2f;
    public float stoppingDistance = 0.8f;
    public float searchDuration = 3f;
    public float memoryDuration = 2f;
    public float collisionRayDistance = 0.4f; // pienempi = reagoi nopeammin

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
            Vector2 toPlayer = targetPos - rb.position;
            float distance = toPlayer.magnitude;

            moveDir = toPlayer.normalized;

            if (distance > stoppingDistance)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                currentSpeed = 0f; // pysähdy, mutta käänny silti pelaajaan
            }

            // === Lightweight obstacle avoidance ===
            moveDir = ObstacleAvoidance(moveDir);
        }
        // === SEARCH LAST KNOWN POSITION ===
        else if (isSearching)
        {
            searchTimer -= Time.fixedDeltaTime;
            Vector2 dir = (lastKnownPlayerPos - rb.position).normalized;
            moveDir = ObstacleAvoidance(dir);

            rb.linearVelocity = moveDir * walkSpeed * 0.5f;

            if (Vector2.Distance(rb.position, lastKnownPlayerPos) < 0.3f || searchTimer <= 0)
            {
                isSearching = false;
                rb.linearVelocity = Vector2.zero;
                ChooseNewDirection();
            }

            currentSpeed = rb.linearVelocity.magnitude;
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

                moveDir = ObstacleAvoidance(moveDir);
            }
        }

        // === ROTATE ===
        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            float smooth = Mathf.LerpAngle(rb.rotation, angle, 10f * Time.fixedDeltaTime);
            rb.rotation = smooth;
        }

        rb.linearVelocity = moveDir * currentSpeed;
        anim?.SetBool("bool_run", currentSpeed > 0.1f);
    }

    Vector2 ObstacleAvoidance(Vector2 dir)
    {
        RaycastHit2D wallHit = Physics2D.Raycast(rb.position, dir, collisionRayDistance, LayerMask.GetMask("Wall"));
        if (wallHit.collider != null)
        {
            Vector2 left = new Vector2(-dir.y, dir.x);
            Vector2 right = new Vector2(dir.y, -dir.x);

            RaycastHit2D leftHit = Physics2D.Raycast(rb.position, left, collisionRayDistance, LayerMask.GetMask("Wall"));
            RaycastHit2D rightHit = Physics2D.Raycast(rb.position, right, collisionRayDistance, LayerMask.GetMask("Wall"));

            if (leftHit.collider == null) return left;
            else if (rightHit.collider == null) return right;
            else return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // fallback
        }
        return dir;
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

    public void SetTarget(Transform t) => OnPlayerSpotted(t);
    public void ClearTarget() => OnPlayerLost();
}