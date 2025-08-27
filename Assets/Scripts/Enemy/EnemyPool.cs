using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int _poolSizePerType = 5;

    private List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        foreach (var prefab in _enemyPrefabs)
        {
            for (int i = 0; i < _poolSizePerType; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }
    }

    public GameObject GetEnemy()
    {
        List<GameObject> inactiveEnemies = _pool.FindAll(e => !e.activeInHierarchy);

        if (inactiveEnemies.Count == 0)
        {
            GameObject obj = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)]);
            obj.SetActive(false);
            _pool.Add(obj);
            return obj;
        }

        GameObject enemy = inactiveEnemies[Random.Range(0, inactiveEnemies.Count)];
        return enemy;
    }
}