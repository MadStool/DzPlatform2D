using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float respawnTime = 3f;
    public Transform[] spawnPoints;

    private List<Item> activeItems = new List<Item>();

    private void Start()
    {
        SpawnItems();
    }

    public void ItemPicked(Item collectedCoin)
    {
        activeItems.Remove(collectedCoin);

        Destroy(collectedCoin.gameObject);

        Invoke(nameof(SpawnItems), respawnTime);
    }

    private void SpawnItems()
    {
        activeItems.RemoveAll(item => item == null);

        foreach (var item in activeItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }

        activeItems.Clear();

        foreach (var point in spawnPoints)
        {
            if (point != null)
            {
                var newItem = Instantiate(itemPrefab, point.position, Quaternion.identity);
                Item itemComponent = newItem.GetComponent<Item>();

                if (itemComponent != null)
                {
                    itemComponent.Initialize(this);
                    activeItems.Add(itemComponent);
                }
            }
        }
    }
}