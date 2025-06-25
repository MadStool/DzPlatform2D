using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private const string AnimParaMove = "IsMoving";

    [Header("Patrol settings")]
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;

    [Header("Anim. settings")]
    public Animator animator;
    public bool flipSprite = true;

    private int currentPointIndex = 0;
    private float waitCounter;
    private bool isWaiting = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (patrolPoints.Length > 0)
            transform.position = patrolPoints[0].position;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
                isWaiting = false;

            if (animator != null)
                animator.SetBool(AnimParaMove, false);

            return;
        }

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 moveDirection = (targetPoint.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (animator != null)
        {
            animator.SetBool(AnimParaMove, true);
        }

        if (flipSprite && moveDirection.x != 0)
        {
            spriteRenderer.flipX = moveDirection.x > 0;
        }

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            isWaiting = true;
            waitCounter = waitTime;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}