using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _returnSpeed = 3f;

    [Header("Components")]
    [SerializeField] private PatrolPath _patrolPath;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private EnemyChase _enemyChase;

    private bool _isReturningToPath;
    private Vector2 _targetReturnPosition;

    private void Update()
    {
        if (_enemyChase.CanSeePlayer() &&
            Vector2.Distance(transform.position, _enemyChase.Player.transform.position) <= _enemyChase.DetectionRadius)
        {
            _enemyChase.StartChasing(_patrolPath.CurrentPoint);
            _isReturningToPath = false;

            return;
        }

        if (_enemyChase.IsChasing)
        {
            if (_enemyChase.HasLineOfSight == false)
            {
                _isReturningToPath = true;
                _targetReturnPosition = _enemyChase.LastPatrolPosition;
                _enemyChase.StopChasing();
            }

            return;
        }

        if (_isReturningToPath)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                _targetReturnPosition,
                _returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _targetReturnPosition) > 0.1f)
            {
                _isReturningToPath = false;
            }

            return;
        }

        Vector3 direction = _patrolPath.UpdatePath(transform, _moveSpeed);
        _characterAnimator.UpdateAnimation(direction, !_patrolPath.IsWaiting);
    }
}