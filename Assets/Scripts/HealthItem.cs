using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthItem : Item
{
    [SerializeField] private int _healAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Heal(_healAmount);

            Destroy(gameObject);
        }
    }
}