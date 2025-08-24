using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 2f;

    private float _nextFireTime;
    private Camera _mainCamera;
    private bool _spriteLooksLeft = true;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        AimAtCursor();

        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void AimAtCursor()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_spriteLooksLeft)
            angle += 180f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector2 direction = _firePoint.right;
        if (_spriteLooksLeft)
            direction *= -1;

        bullet.SetDirection(direction);
    }
}
