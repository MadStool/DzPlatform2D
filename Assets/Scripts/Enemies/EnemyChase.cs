using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private Player _player;

    private Rigidbody2D _rigidbody;
    private bool _isChasing;

    public float DetectionRadius => _detectionRadius;
    public Player Player => _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public bool CanSeePlayer()
    {
        if (_player == null)
        {
            Debug.LogWarning("Player reference is missing in EnemyChase");

            return false;
        }

        Vector2 toPlayer = _player.transform.position - transform.position;
        float distanceSqr = toPlayer.sqrMagnitude;

        if (distanceSqr > _detectionRadius * _detectionRadius)
            return false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            toPlayer.normalized,
            Mathf.Sqrt(distanceSqr));

        foreach (var hit in hits)
        {
            if (hit.collider.isTrigger || hit.collider.gameObject == gameObject)
                continue;

            if (hit.collider.GetComponent<Player>() != null)
                return true;

            return false;
        }

        return false;
    }

    public void StartChasing()
    {
        _isChasing = true;
    }

    public void StopChasing()
    {
        _isChasing = false;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void UpdateChase()
    {
        if (_isChasing == false || _player == null)
            return;

        Vector2 direction = (_player.transform.position - transform.position).normalized;
        direction.y = 0;
        _rigidbody.linearVelocity = direction * _chaseSpeed;
    }
}