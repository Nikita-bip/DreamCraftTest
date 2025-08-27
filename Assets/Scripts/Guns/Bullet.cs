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

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector2 direction, float speed)
    {
        if (rb != null)
            rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
            Destroy(gameObject);
    }
}