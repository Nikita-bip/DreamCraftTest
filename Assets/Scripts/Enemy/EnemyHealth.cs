using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 5;

    [Header("UI Elements")]
    [SerializeField] private Slider _healthSlider;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = (float)_currentHealth / _maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
            _currentHealth = 0;

        UpdateHealthUI();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}