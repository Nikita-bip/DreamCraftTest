using UnityEngine;
using System.Collections;

public class AssaultRifle : WeaponBase
{
    [SerializeField] private int _burstCount = 3;
    [SerializeField] private float _burstDelay = 0.1f;

    private bool _isShooting = false;

    public override void Shoot()
    {
        if (!_isShooting)
            StartCoroutine(BurstFire());
    }

    private IEnumerator BurstFire()
    {
        _isShooting = true;

        for (int i = 0; i < _burstCount; i++)
        {
            FireBulletDirect();
            yield return new WaitForSeconds(_burstDelay);
        }

        float remainingDelay = Mathf.Max(0, fireRate - _burstDelay * (_burstCount - 1));
        yield return new WaitForSeconds(remainingDelay);

        _isShooting = false;
    }

    private void FireBulletDirect()
    {
        if (firePoint == null) return;

        GameObject bulletObj = BulletPool.Instance.GetBullet();
        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            Vector2 dir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
            bullet.Launch(dir.normalized, bulletSpeed);
        }
    }

    private void OnDisable()
    {
        _isShooting = false;
    }
}
