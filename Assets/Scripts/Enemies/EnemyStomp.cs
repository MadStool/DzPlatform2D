using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyStomp : MonoBehaviour
{
    [Header("Stomp Settings")]
    [SerializeField] private Collider2D _stompCollider;
    [SerializeField] private int _stompDamage = 20;
    [SerializeField] private float _bounceForce = 8f;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerFootStomp foot = collision.collider.GetComponent<PlayerFootStomp>();

        if (foot != null && collision.otherCollider == _stompCollider)
        {
            _health.TakeDamage(_stompDamage, collision.transform);

            foot.BounceAfterStomp(_bounceForce);
        }
    }
}