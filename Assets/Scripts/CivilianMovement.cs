using UnityEngine;

public class CivilianMovement : MonoBehaviour
{
    [Header("Speed")]
    public float walkSpeed = 2f;
    public float panicMultiplier = 1.6f;
    public float acceleration = 6f;     // nopeuden muutosnopeus

    [Header("Confusion")]
    public float sideWanderStrength = 0.5f;
    public float sideChangeInterval = 0.3f;
    public float afterPanicTime = 1.5f; // kauanko jatkaa poispäin triggerin jälkeen

    [Header("Wander")]
    public float changeTime = 2f;

    private Vector2 direction;
    private Vector2 lastFleeDirection;
    private Vector2 sideDir;

    private float timer;
    private float sideTimer;
    private float baseSpeed;
    private float currentSpeed;
    private float afterPanicTimer;

    private bool seesThreat;
    private Transform threat;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = walkSpeed;
        currentSpeed = walkSpeed;
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        sideTimer -= Time.fixedDeltaTime;

        if (seesThreat && threat != null)
        {
            HandlePanic();
            return;
        }

        if (afterPanicTimer > 0f)
        {
            HandleAfterPanic();
            return;
        }

        HandleWander();
    }

    void HandlePanic()
    {
        Vector2 fleeDir = (rb.position - (Vector2)threat.position).normalized;
        lastFleeDirection = fleeDir;

        UpdateSideDirection();

        Vector2 moveDir = (fleeDir + sideDir * sideWanderStrength).normalized;

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            walkSpeed * panicMultiplier,
            acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = moveDir * currentSpeed;
    }

    void HandleAfterPanic()
    {
        UpdateSideDirection();

        Vector2 moveDir =
            (lastFleeDirection + sideDir * sideWanderStrength).normalized;

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            walkSpeed,
            acceleration * Time.fixedDeltaTime
        );

        afterPanicTimer -= Time.fixedDeltaTime;
        rb.linearVelocity = moveDir * currentSpeed;
    }

    void HandleWander()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0f)
            ChooseNewDirection();

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            walkSpeed,
            acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = direction * currentSpeed;
    }

    void UpdateSideDirection()
    {
        if (sideTimer <= 0f)
        {
            sideTimer = sideChangeInterval;

            sideDir = Random.value < 0.5f
                ? new Vector2(-lastFleeDirection.y, lastFleeDirection.x)
                : new Vector2(lastFleeDirection.y, -lastFleeDirection.x);
        }
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

    public void SetThreat(Transform t)
    {
        seesThreat = true;
        threat = t;
    }

    public void ClearThreat()
    {
        seesThreat = false;
        threat = null;

        afterPanicTimer = afterPanicTime;
    }
}
