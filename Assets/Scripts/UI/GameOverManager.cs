using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(false);

        if (_playerHealth != null)
            _playerHealth.OnDeath.AddListener(ShowGameOver);
    }

    private void ShowGameOver()
    {
        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}