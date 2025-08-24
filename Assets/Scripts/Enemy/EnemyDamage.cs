using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackRate = 2f;

    private float _nextAttackTime = 0f;
    private Transform _player;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        if (_playerHealth != null)
            _player = _playerHealth.transform;
    }

    private void Update()
    {
        if (_player == null) return;

        float distance = Vector2.Distance(transform.position, _player.position);
        if (distance <= _attackRange && Time.time >= _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + _attackRate;
        }
    }

    private void Attack()
    {
        if (_playerHealth != null)
        {
            _playerHealth.TakeDamage(_damage);
            Debug.Log("Враг ударил игрока!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
