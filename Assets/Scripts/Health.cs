using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private IDamageHandler[] _damageHandlers;

    public event Action<int, int> OnHealthChanged;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
        _damageHandlers = GetComponents<IDamageHandler>();
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        foreach (var handler in _damageHandlers)
        {
            handler.HandleDamage(damage, damageSource);
        }

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        //Debug.Log($"Player healed by {amount}. Health: {_currentHealth}/{_maxHealth}");

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
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