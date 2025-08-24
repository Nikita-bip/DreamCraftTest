using UnityEngine;
using System.Collections;

public class EdgeEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;
    public float spawnOffset = 1f;

    private Camera mainCamera;
    private bool spawning = true;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (spawning)
        {
            SpawnEnemy();

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;

        int side = Random.Range(0, 4);

        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        switch (side)
        {
            case 0: // верх
                spawnPosition = new Vector3(
                    Random.Range(mainCamera.transform.position.x - camWidth / 2, mainCamera.transform.position.x + camWidth / 2),
                    mainCamera.transform.position.y + camHeight / 2 + spawnOffset,
                    0
                );
                break;
            case 1: // низ
                spawnPosition = new Vector3(
                    Random.Range(mainCamera.transform.position.x - camWidth / 2, mainCamera.transform.position.x + camWidth / 2),
                    mainCamera.transform.position.y - camHeight / 2 - spawnOffset,
                    0
                );
                break;
            case 2: // лево
                spawnPosition = new Vector3(
                    mainCamera.transform.position.x - camWidth / 2 - spawnOffset,
                    Random.Range(mainCamera.transform.position.y - camHeight / 2, mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
            case 3: // право
                spawnPosition = new Vector3(
                    mainCamera.transform.position.x + camWidth / 2 + spawnOffset,
                    Random.Range(mainCamera.transform.position.y - camHeight / 2, mainCamera.transform.position.y + camHeight / 2),
                    0
                );
                break;
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning() => spawning = false;
    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }
}
