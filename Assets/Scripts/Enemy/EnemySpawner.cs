using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDelay = 5f;
    [SerializeField] private float maxSpawnDelay = 10f;
    [SerializeField] private float spawnOffset = 1f;

    private Camera _mainCamera;
    private bool _isSpawning = true;

    private void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isSpawning)
        {
            SpawnEnemy();

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyObj = EnemyPool.Instance.GetEnemy();
        enemyObj.transform.position = GetSpawnPosition();
        enemyObj.transform.rotation = Quaternion.identity;
        enemyObj.SetActive(true);

        EnemyHealth health = enemyObj.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.SendMessage("ResetHealth", SendMessageOptions.DontRequireReceiver);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        int side = Random.Range(0, 4);

        float camHeight = 2f * _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        switch (side)
        {
            case 0: // верх
                spawnPosition = new Vector3(
                    Random.Range(_mainCamera.transform.position.x - camWidth / 2, _mainCamera.transform.position.x + camWidth / 2),
                    _mainCamera.transform.position.y + camHeight / 2 + spawnOffset,
                    0
                );
                break;
            case 1: // низ
                spawnPosition = new Vector3(
                    Random.Range(_mainCamera.transform.position.x - camWidth / 2, _mainCamera.transform.position.x + camWidth / 2),
                    _mainCamera.transform.position.y - camHeight / 2 - spawnOffset,
                    0
                );
                break;
            case 2: // лево
                spawnPosition = new Vector3(
                    _mainCamera.transform.position.x - camWidth / 2 - spawnOffset,
                    Random.Range(_mainCamera.transform.position.y - camHeight / 2, _mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
            case 3: // право
                spawnPosition = new Vector3(
                    _mainCamera.transform.position.x + camWidth / 2 + spawnOffset,
                    Random.Range(_mainCamera.transform.position.y - camHeight / 2, _mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
        }

        return spawnPosition;
    }

    public void StopSpawning() => _isSpawning = false;

    public void StartSpawning()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }
}