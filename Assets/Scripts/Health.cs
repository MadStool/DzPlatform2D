using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private IDamageHandler[] _damageHandlers;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _damageHandlers = GetComponents<IDamageHandler>();
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);

        foreach (var handler in _damageHandlers)
        {
            handler.HandleDamage(damage, damageSource);
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        Debug.Log($"Player healed by {amount}. Health: {_currentHealth}/{_maxHealth}");
    }

    private void Die()
    {
        foreach (var handler in _damageHandlers)
        {
            handler.HandleDeath();
        }

        Debug.Log("Player died!");
    }
}