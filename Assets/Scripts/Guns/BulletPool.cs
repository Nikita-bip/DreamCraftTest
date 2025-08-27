using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> bullets = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bullets.Enqueue(obj);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count == 0)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bullets.Enqueue(obj);
        }

        GameObject bullet = bullets.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
