using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Item>(out Item item) &&
           other.TryGetComponent<HealthItem>(out HealthItem healthItem) == false)
        {
            Destroy(item.gameObject);
        }
    }
}