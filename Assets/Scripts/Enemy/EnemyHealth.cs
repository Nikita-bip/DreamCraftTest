using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private Slider healthSlider;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)_currentHealth / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
            _currentHealth = 0;

        UpdateHealthUI();

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}