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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShooting = GetComponent<EnemyShooting>();

        currentSpeed = walkSpeed;
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        Vector2 moveDir = Vector2.zero;

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
                return;
            }
        }
        else
        {
            timer -= Time.fixedDeltaTime;

            if (timer <= 0)
                ChooseNewDirection();

            moveDir = direction;

            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                walkSpeed,
                acceleration * Time.fixedDeltaTime
            );
        }

        rb.linearVelocity = moveDir * currentSpeed;
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