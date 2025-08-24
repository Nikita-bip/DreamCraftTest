using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    private void Start()
    {
        if (_playerHealth != null)
            _playerHealth.OnHealthChanged.AddListener(UpdateHearts);
    }

    private void UpdateHearts(int current, int max)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < current)
                _hearts[i].sprite = _fullHeart;
            else
                _hearts[i].sprite = _emptyHeart;
        }
    }
}