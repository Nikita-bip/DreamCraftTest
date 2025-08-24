using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _lifetime = 10f;
    [SerializeField] private int _damage = 1;

    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}