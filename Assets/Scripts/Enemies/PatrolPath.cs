using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    private const int NextPointIncrement = 1;
    private const float ReachThresholdSqr = 0.01f;

    [SerializeField] private Transform[] _points;
    [SerializeField] private float _waitTime = 1f;

    private int _currentIndex;
    private float _waitTimer;
    private bool _isWaiting;

    public bool IsWaiting => _isWaiting;
    public Vector3 CurrentPoint => _points[_currentIndex].position;

    public Vector3 UpdatePath(Transform entity, float speed)
    {
        if (_isWaiting)
        {
            _waitTimer -= Time.deltaTime;

            if (_waitTimer <= 0)
                _isWaiting = false;

            return Vector3.zero;
        }

        Vector3 toTarget = CurrentPoint - entity.position;
        Vector3 horizontalMove = new Vector3(toTarget.x, 0, 0).normalized;
        entity.position += horizontalMove * speed * Time.deltaTime;

        if (new Vector3(toTarget.x, 0, 0).sqrMagnitude <= ReachThresholdSqr)
        {
            _isWaiting = true;
            _waitTimer = _waitTime;
            _currentIndex = (_currentIndex + NextPointIncrement) % _points.Length;
        }

        return horizontalMove;
    }
}