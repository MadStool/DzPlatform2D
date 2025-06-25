using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemSpawner spawner;

    public void Initialize(ItemSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMove>(out PlayerMove playerMove))
        {
            spawner?.ItemPicked(this);
        }
    }
}