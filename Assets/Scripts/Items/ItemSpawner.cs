using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private float _respawnTime = 3f;
    [SerializeField] private Transform[] _spawnPoints;

    private bool _isActive = true;
    private readonly List<Item> _activeItems = new List<Item>();
    private readonly Queue<Vector2> _positionsToRespawn = new Queue<Vector2>();
    private Coroutine _respawnCoroutine;

    private void Start()
    {
        if (ValidateSetup())
            SpawnAllItems();
    }

    private bool ValidateSetup()
    {
        if (_itemPrefab == null)
        {
            Debug.LogError("Item prefab is not assigned!", this);
            return false;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!", this);
            return false;
        }

        return true;
    }

    public void ReportItemCollected(Vector2 spawnPosition)
    {
        if (_isActive == false)
             return;

        _positionsToRespawn.Enqueue(spawnPosition);

        if (_respawnCoroutine == null)
            _respawnCoroutine = StartCoroutine(RespawnProcess());
    }

    private IEnumerator RespawnProcess()
    {
        while (_isActive && _positionsToRespawn.Count > 0)
        {
            yield return new WaitForSeconds(_respawnTime);

            if (_isActive == false)
                yield break;

            SpawnItemAtPoint(_positionsToRespawn.Dequeue());
        }

        _respawnCoroutine = null;
    }

    private void SpawnAllItems()
    {
        ClearAllItems();

        foreach (var point in _spawnPoints)
        {
            if (point == null)
            {
                Debug.LogWarning("Spawn point is null!", this);
                continue;
            }

            SpawnItemAtPoint(point.position);
        }
    }

    private void SpawnItemAtPoint(Vector2 position)
    {
        Item newItem = Instantiate(_itemPrefab, position, Quaternion.identity);
        _activeItems.Add(newItem);

        if (newItem.TryGetComponent(out ItemEventBridge bridge) == false)
        {
            Debug.LogWarning($"Item prefab {_itemPrefab.name} is missing ItemEventBridge!", this);
            return;
        }

        bridge.Initialize(this, position);
    }

    private void ClearAllItems()
    {
        foreach (var item in _activeItems)
            if (item != null)
                Destroy(item.gameObject);

        _activeItems.Clear();
    }

    private void OnDestroy()
    {
        _isActive = false;

        if (_respawnCoroutine != null)
            StopCoroutine(_respawnCoroutine);
    }
}