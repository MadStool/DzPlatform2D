using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyAI : MonoBehaviour
{
    private enum EnemyState { Patrol, Chase, Return }

    [Header("Settings")]
    [SerializeField] private float _chaseStopDistance = 6f;
    [SerializeField] private float _detectionCooldown = 0.2f;
    [SerializeField] private Player _player;

    private EnemyState _currentState = EnemyState.Patrol;
    private EnemyPatrol _patrol;
    private EnemyChase _chase;
    private SpriteRenderer _spriteRenderer;
    private CharacterAnimator _animator;
    private Vector2 _returnTarget;
    private float _lastDetectionTime;

    private void Awake()
    {
        _patrol = GetComponent<EnemyPatrol>();
        _chase = GetComponent<EnemyChase>();
        _chase.SetPlayer(_player);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<CharacterAnimator>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Chase:
                ChaseUpdate();
                break;
            case EnemyState.Return:
                ReturnUpdate();
                break;
        }
    }

    private void PatrolUpdate()
    {
        Vector3 direction = _patrol.UpdatePatrol();
        bool isMoving = _patrol.IsWaiting == false;

        if (Mathf.Abs(direction.x) > 0.1f)
        {
            _spriteRenderer.flipX = direction.x > 0;
        }

        _animator.UpdateAnimation(direction, isMoving);

        if (Time.time - _lastDetectionTime > _detectionCooldown)
        {
            _lastDetectionTime = Time.time;

            if (_chase.CanSeePlayer())
            {
                _returnTarget = _patrol.GetCurrentPatrolPoint();
                _currentState = EnemyState.Chase;
                _chase.StartChasing();
            }
        }
    }

    private void ChaseUpdate()
    {
        _chase.UpdateChase();
        Vector2 direction = Vector2.zero;

        if (_chase.Player != null)
        {
            direction = _chase.Player.transform.position - transform.position;
            _spriteRenderer.flipX = direction.x > 0;
        }

        _animator.UpdateAnimation(direction.normalized, true);

        if (_chase.Player == null ||
            Vector2.Distance(transform.position, _chase.Player.transform.position) > _chaseStopDistance)
        {
            _currentState = EnemyState.Return;
            _chase.StopChasing();
        }
    }

    private void ReturnUpdate()
    {
        Vector2 toTarget = _returnTarget - (Vector2)transform.position;
        Vector2 horizontalMove = new Vector2(toTarget.x, 0);
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(_returnTarget.x, transform.position.y),
            _patrol.ReturnSpeed * Time.deltaTime
        );

        _spriteRenderer.flipX = horizontalMove.x > 0;
        _animator.UpdateAnimation(horizontalMove.normalized, true);

        if (horizontalMove.sqrMagnitude <= 0.01f)
        {
            _currentState = EnemyState.Patrol;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<EnemyChase>().DetectionRadius);
    }
}