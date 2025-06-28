using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private float _respawnTime = 3f;
    [SerializeField] private Transform[] _spawnPoints;

    private bool _isActive = true;

    private readonly List<Item> _activeItems = new List<Item>();
    private readonly Queue<Vector2> _positionsToRespawn = new Queue<Vector2>();

    private void Start()
    {
        if (_itemPrefab == null)
        {
            Debug.LogError("Item prefab is not assigned in the inspector!", this);
            return;
        }

        SpawnAllItems();
    }

    public void ReportItemCollected(Vector2 spawnPosition)
    {
        if (_isActive == false) 
             return;

        _positionsToRespawn.Enqueue(spawnPosition);

        if (_positionsToRespawn.Count == 1)
        {
            Invoke(nameof(RespawnNextPosition), _respawnTime);
        }
    }

    private void SpawnAllItems()
    {
        ClearAllItems();

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!", this);
            return;
        }

        foreach (var point in _spawnPoints)
        {
            if (point == null)
            {
                Debug.LogWarning("One of spawn points is null!", this);
                continue;
            }

            SpawnItemAtPoint(point.position);
        }
    }

    private void RespawnNextPosition()
    {
        if (_positionsToRespawn.Count == 0)
            return;

        var position = _positionsToRespawn.Dequeue();
        SpawnItemAtPoint(position);

        if (_positionsToRespawn.Count > 0)
        {
            Invoke(nameof(RespawnNextPosition), _respawnTime);
        }
    }

    private void SpawnItemAtPoint(Vector2 position)
    {
        if (_itemPrefab == null)
        {
            Debug.LogError("Cannot spawn item - prefab is null!", this);
            return;
        }

        Item newItem = Instantiate(_itemPrefab, position, Quaternion.identity);
        _activeItems.Add(newItem);

        ItemEventBridge bridge = newItem.GetComponent<ItemEventBridge>();

        if (bridge == null)
        {
            Debug.LogWarning($"Item prefab {_itemPrefab.name} is missing ItemEventBridge component!", this);
            return;
        }

        bridge.Initialize(this, position);
    }

    private void ClearAllItems()
    {
        foreach (var item in _activeItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }

        _activeItems.Clear();
    }

    private void OnDestroy()
    {
        _isActive = false;
        CancelInvoke();
    }
}