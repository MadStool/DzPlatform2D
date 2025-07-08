using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _returnSpeed = 3f;
    [SerializeField] private PatrolPath _patrolPath;

    public float ReturnSpeed => _returnSpeed;
    public bool IsWaiting => _patrolPath.IsWaiting;
    public Vector2 GetCurrentPatrolPoint() => _patrolPath.CurrentPoint;

    public Vector3 UpdatePatrol()
    {
        return _patrolPath.UpdatePath(transform, _moveSpeed);
    }
}