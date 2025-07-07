using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    private const int NextPointIncrement = 1;
    private const float ReachThreshold = 0.1f;

    [SerializeField] private Transform[] _points;
    [SerializeField] private float _waitTime = 1f;

    private int _currentIndex;
    private float _waitTimer;
    private bool _isWaiting;

    public Transform[] Points => _points;
    public Vector3 CurrentPoint => _points[_currentIndex].position;
    public bool IsWaiting => _isWaiting;

    public void Initialize(Transform entity)
    {
        if (_points.Length > 0)
            entity.position = _points[0].position;
    }

    public Vector3 UpdatePath(Transform entity, float moveSpeed)
    {
        if (_isWaiting)
        {
            _waitTimer -= Time.deltaTime;

            if (_waitTimer <= 0) 
                _isWaiting = false;

            return Vector3.zero;
        }

        Vector3 direction = (CurrentPoint - entity.position).normalized;
        entity.position = Vector3.MoveTowards(entity.position, CurrentPoint, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(entity.position, CurrentPoint) < ReachThreshold)
        {
            _isWaiting = true;
            _waitTimer = _waitTime;
            _currentIndex = (_currentIndex + NextPointIncrement) % _points.Length;
        }

        return direction;
    }
}