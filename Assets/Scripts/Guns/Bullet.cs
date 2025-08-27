using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
        rb.velocity = Vector2.zero;
    }

    public void Launch(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            ReturnToPool();
        }

        if (other.CompareTag("Wall"))
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}
