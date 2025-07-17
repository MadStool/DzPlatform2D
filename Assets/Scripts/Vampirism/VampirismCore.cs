using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampirismCore : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] private float _abilityRadius = 5f;
    [SerializeField] private float _healthStealPerSecond = 15f;
    [SerializeField] private float _maxStealPerFrame = 2f;

    private Transform _nearestEnemy;
    private Health _playerHealth;
    private float _damageAccumulator;
    private float _radiusSqr;

    public float AbilityRadius => _abilityRadius;
    public Transform NearestEnemy => _nearestEnemy;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _radiusSqr = _abilityRadius * _abilityRadius;
    }

    public void FindNearestEnemy()
    {
        _nearestEnemy = null;
        float closestDistanceSqr = float.MaxValue;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _abilityRadius);

        foreach (var hit in hits)
        {
            if (hit.isTrigger || hit.gameObject == gameObject)
                continue;

            if (hit.TryGetComponent(out Enemy _))
            {
                Vector2 direction = hit.transform.position - transform.position;
                float distanceSqr = direction.sqrMagnitude;

                if (distanceSqr < closestDistanceSqr && distanceSqr <= _radiusSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    _nearestEnemy = hit.transform;
                }
            }
        }
    }

    public void StealHealth()
    {
        if (_nearestEnemy == null)
            return;

        if (_nearestEnemy.TryGetComponent(out Health enemyHealth))
        {
            _damageAccumulator += _healthStealPerSecond * Time.deltaTime;
            float healthToSteal = Mathf.Min(_damageAccumulator, _maxStealPerFrame);

            if (healthToSteal >= 1f && enemyHealth.CurrentHealth > 0)
            {
                int stealAmount = Mathf.FloorToInt(healthToSteal);
                enemyHealth.TakeDamage(stealAmount, transform);
                _playerHealth.Heal(stealAmount);
                _damageAccumulator -= stealAmount;
            }
        }
    }

    public void ResetTarget()
    {
        _nearestEnemy = null;
    }
}