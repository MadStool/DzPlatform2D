using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampirismCore : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] private float _abilityRadius = 5f;
    [SerializeField] private float _healthStealPerSecond = 15f;

    private Transform _nearestEnemy;
    private Health _playerHealth;
    private float _damageAccumulator;

    public float AbilityRadius => _abilityRadius;
    public Transform NearestEnemy => _nearestEnemy;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    public void FindNearestEnemy()
    {
        _nearestEnemy = null;
        float closestDistance = float.MaxValue;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _abilityRadius);

        foreach (var hit in hits)
        {
            if (hit.isTrigger || hit.gameObject == gameObject) 
                continue;

            if (hit.TryGetComponent(out Enemy _))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
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

            float maxStealPerFrame = 2f;
            float healthToSteal = Mathf.Min(_damageAccumulator, maxStealPerFrame);

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