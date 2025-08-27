using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _minSpawnDelay = 5f;
    [SerializeField] private float _maxSpawnDelay = 10f;
    [SerializeField] private float _spawnOffset = 1f;

    private Camera _mainCamera;
    private bool _spawning = true;

    private void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_spawning)
        {
            SpawnEnemy();

            float delay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyPrefabs.Length == 0) return;

        GameObject enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];

        Vector3 spawnPosition = Vector3.zero;
        int side = Random.Range(0, 4);

        float camHeight = 2f * _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        switch (side)
        {
            case 0: // верх
                spawnPosition = new Vector3(
                    Random.Range(_mainCamera.transform.position.x - camWidth / 2, _mainCamera.transform.position.x + camWidth / 2),
                    _mainCamera.transform.position.y + camHeight / 2 + _spawnOffset,
                    0
                );
                break;
            case 1: // низ
                spawnPosition = new Vector3(
                    Random.Range(_mainCamera.transform.position.x - camWidth / 2, _mainCamera.transform.position.x + camWidth / 2),
                    _mainCamera.transform.position.y - camHeight / 2 - _spawnOffset,
                    0
                );
                break;
            case 2: // лево
                spawnPosition = new Vector3(
                    _mainCamera.transform.position.x - camWidth / 2 - _spawnOffset,
                    Random.Range(_mainCamera.transform.position.y - camHeight / 2, _mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
            case 3: // право
                spawnPosition = new Vector3(
                    _mainCamera.transform.position.x + camWidth / 2 + _spawnOffset,
                    Random.Range(_mainCamera.transform.position.y - camHeight / 2, _mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning() => _spawning = false;

    public void StartSpawning()
    {
        if (!_spawning)
        {
            _spawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }
}
