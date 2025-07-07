using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private Ground _groundMarker;

    [Header("References")]
    [SerializeField] private Player _player;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private Rigidbody2D _rb;
    private bool _isChasing;
    private bool _hasLineOfSight;
    private Vector2 _lastPatrolPosition;

    public bool IsChasing => _isChasing;
    public float DetectionRadius => _detectionRadius;
    public Player Player => _player;
    public bool HasLineOfSight => _hasLineOfSight;
    public Vector2 LastPatrolPosition => _lastPatrolPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D is missing!", this);
            return;
        }

        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;

        if (_player == null)
            Debug.LogWarning("Player reference is not set!", this);
    }

    public void StartChasing(Vector2 patrolPosition)
    {
        _isChasing = true;
        _lastPatrolPosition = patrolPosition;
    }

    public void StopChasing()
    {
        _isChasing = false;
        _rb.linearVelocity = Vector2.zero;
    }

    public bool CanSeePlayer()
    {
        if (_player == null) 
            return false;

        Vector2 direction = _player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > _detectionRadius) 
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction.normalized,
            distance
        );

        return hit.collider == null ||
               hit.collider.GetComponent<Player>() != null ||
               hit.collider.GetComponent<Ground>() == null;
    }

    private void Update()
    {
        if (_isChasing == false || _player == null)
            return;

        float directionX = Mathf.Sign(_player.transform.position.x - transform.position.x);
        _rb.linearVelocity = new Vector2(directionX * _chaseSpeed, 0);

        if (_characterAnimator != null)
        {
            _characterAnimator.UpdateAnimation(new Vector2(directionX, 0), true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}