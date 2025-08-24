using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthChangedEvent : UnityEvent<int, int> { }

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;

    public HealthChangedEvent OnHealthChanged;
    public UnityEvent OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
        Time.timeScale = 0f;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
            Die();
    }
}