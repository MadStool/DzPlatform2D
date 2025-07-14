using UnityEngine;

public class HealthButtons : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private int healAmount = 5;

    public void OnDamageButton()
    {
        if (health != null)
            health.TakeDamage(damageAmount, null);
    }

    public void OnHealButton()
    {
        if (health != null)
            health.Heal(healAmount);
    }
}