using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private float _angleOffset = 0f;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        UpdateAim();
    }

    private void UpdateAim()
    {
        if (_mainCamera == null) return;

        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + _angleOffset);

        Vector3 localScale = transform.localScale;
        if (angle + _angleOffset > 90 || angle + _angleOffset < -90)
            localScale.y = -Mathf.Abs(localScale.y);
        else
            localScale.y = Mathf.Abs(localScale.y);
        transform.localScale = localScale;
    }
}