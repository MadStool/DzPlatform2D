using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 1f;

    private float _lastAttackTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time < _lastAttackTime + _attackCooldown)
        {
            return;
        }

        Health playerHealth = collision.collider.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damage, transform);
            _lastAttackTime = Time.time;
        }
    }
}