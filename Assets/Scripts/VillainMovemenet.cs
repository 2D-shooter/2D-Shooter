using UnityEngine;

public class VillainMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float acceleration = 8f;   // säädettävä kiihtyvyys
    public float changeTime = 2f;

    private Transform target;
    private Vector2 direction;
    private float timer;
    private bool seesTarget;
    private float currentSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        Vector2 moveDir;

        if (seesTarget && target != null)
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
    }

    public void ClearTarget()
    {
        seesTarget = false;
        target = null;
    }
}
