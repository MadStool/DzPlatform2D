using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private PlayerKnockback _playerKnockback;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _playerKnockback = GetComponent<PlayerKnockback>();
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);

        bool knockFromRight = damageSource.position.x > transform.position.x;
        _playerKnockback.ApplyKnockback(knockFromRight);

        Debug.Log($"Player took {damage} damage! Health: {_currentHealth}/{_maxHealth}");

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        Debug.Log($"Player healed by {amount}. Health: {_currentHealth}/{_maxHealth}");
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }
}