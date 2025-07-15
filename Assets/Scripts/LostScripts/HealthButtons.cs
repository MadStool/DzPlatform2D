using UnityEngine;

public class HealthButtons : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _damageAmount = 30;
    [SerializeField] private int _healAmount = 30;

    public void OnDamageButton()
    {
        if (_health != null)
            _health.TakeDamage(_damageAmount, null);
    }

    public void OnHealButton()
    {
        if (_health != null)
            _health.Heal(_healAmount);
    }
}