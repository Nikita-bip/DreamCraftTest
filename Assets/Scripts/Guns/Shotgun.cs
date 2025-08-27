using UnityEngine;

public class Shotgun : WeaponBase
{
    public int pelletCount = 5;
    public float spreadAngle = 15f;

    public override void Shoot()
    {
        if (Time.time < nextFireTime) return;

        for (int i = 0; i < pelletCount; i++)
        {
            float angleOffset = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rot = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);

            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, rot);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                Vector2 dir = (rot * Vector3.right).normalized;
                bullet.Launch(dir, bulletSpeed);
            }
        }

        nextFireTime = Time.time + fireRate;
    }
}