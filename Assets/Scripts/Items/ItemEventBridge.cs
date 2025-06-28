using UnityEngine;

public class ItemEventBridge : MonoBehaviour
{
    private ItemSpawner _spawner;
    private Vector2 _spawnPosition;
    private bool _applicationQuitting = false;

    private void OnApplicationQuit()
    {
        _applicationQuitting = true;
    }

    public void Initialize(ItemSpawner spawner, Vector2 spawnPosition)
    {
        _spawner = spawner;
        _spawnPosition = spawnPosition;
    }

    private void OnDestroy()
    {
        if (ShouldSkipReport())
            return;

        if (_spawner != null && _spawner.gameObject != null)
        {
            _spawner.ReportItemCollected(_spawnPosition);
        }
    }

    private bool ShouldSkipReport()
    {
        return _applicationQuitting || _spawner == null;
    }
}