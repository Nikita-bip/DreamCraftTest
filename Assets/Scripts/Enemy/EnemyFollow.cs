using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stopDistance = 1f;

    private Rigidbody2D _rb;
    private Transform _player;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            _player = playerMovement.transform;
        }
        else
        {
            Debug.LogWarning("Игрок с компонентом PlayerMovement не найден!");
        }
    }

    private void FixedUpdate()
    {
        if (_player == null) return;

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance > _stopDistance)
        {
            Vector2 direction = (_player.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + direction * _speed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
}