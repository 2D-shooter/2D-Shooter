using UnityEngine;

public class VillainMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float acceleration = 8f;
    public float changeTime = 2f;
    public float stoppingDistance = 0.8f;

    private Transform target;
    private Vector2 direction;
    private float timer;
    private bool seesTarget;
    private float currentSpeed;

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

        // Hakee Animatorin modelista (child)
        anim = GetComponentInChildren<Animator>();

        currentSpeed = walkSpeed;
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        // === SEINÄTÖRMÄYKSEN COOLDOWN ===
        if (collisionCooldown > 0)
        {
            collisionCooldown -= Time.fixedDeltaTime;

            currentSpeed = 0f;
            rb.linearVelocity = Vector2.zero;

            if (anim != null)
                anim.SetBool("bool_run", false);

            return;
        }

        Vector2 moveDir = Vector2.zero;

        // === CHASE ===
        if (seesTarget && target != null)
        {
            float distance = Vector2.Distance(rb.position, target.position);

            if (distance > stoppingDistance)
            {
                moveDir = ((Vector2)target.position - rb.position).normalized;

                currentSpeed = Mathf.MoveTowards(
                    currentSpeed,
                    runSpeed,
                    acceleration * Time.fixedDeltaTime
                );
            }
            else
            {
                currentSpeed = 0f;
                rb.linearVelocity = Vector2.zero;

                if (anim != null)
                    anim.SetBool("bool_run", false);

                return;
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

                currentSpeed = Mathf.MoveTowards(
                    currentSpeed,
                    walkSpeed,
                    acceleration * Time.fixedDeltaTime
                );

                if (timer <= 0)
                {
                    isIdle = true;
                    idleTimer = idleTime;
                }
            }
        }

        // === KÄÄNTYMINEN ===
        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }

        // === LIIKE ===
        rb.linearVelocity = moveDir * currentSpeed;

        // === ANIMAATIO ===
        if (anim != null)
        {
            anim.SetBool("bool_run", currentSpeed > 0.1f);
        }

        Debug.Log("villain_speed " + currentSpeed);
    }

    void ChooseNewDirection()
    {
        timer = changeTime;

        Vector2[] dirs =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        direction = dirs[Random.Range(0, dirs.Length)];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // uusi suunta pois seinästä
            Vector2 normal = collision.contacts[0].normal;
            ChooseNewDirection();

            // pysäytä
            currentSpeed = 0f;
            rb.linearVelocity = Vector2.zero;

            // pieni tauko ennen liikkumista
            collisionCooldown = 0.2f;
        }
    }

    public void SetTarget(Transform t)
    {
        seesTarget = true;
        target = t;

        if (enemyShooting != null)
            enemyShooting.SetTarget(t);
    }

    public void ClearTarget()
    {
        seesTarget = false;
        target = null;

        if (enemyShooting != null)
            enemyShooting.ClearTarget();
    }
}