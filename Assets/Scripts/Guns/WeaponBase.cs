using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;
    public float fireRate = 0.5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    protected float nextFireTime;

    private void Start()
    {
        RotateToMouse();
    }

    private void Update()
    {
        RotateToMouse();
    }

    private void OnEnable()
    {
        RotateToMouse();
    }

    private void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float angleOffset = 0f;
        transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);

        Vector3 localScale = transform.localScale;
        if (angle + angleOffset > 90 || angle + angleOffset < -90)
            localScale.y = -Mathf.Abs(localScale.y);
        else
            localScale.y = Mathf.Abs(localScale.y);
        transform.localScale = localScale;
    }

    protected void FireBullet()
    {
        if (Time.time < nextFireTime) return;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            Vector2 dir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
            bullet.Launch(dir.normalized, bulletSpeed);
        }

        nextFireTime = Time.time + fireRate;
    }

    public abstract void Shoot();
}