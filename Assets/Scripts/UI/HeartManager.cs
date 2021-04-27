using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _halfFullHeart;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private FloatValue _heartContainer;
    [SerializeField] private FloatValue _playerCurrentHealth;

    private float _tempHealth;

    private void Start()
    {
        InitHearts();
        
    }

    private void InitHearts()
    {
        for (int i = 0; i < _heartContainer.initialValue; i++)
        {
            _hearts[i].gameObject.SetActive(true);
            _hearts[i].sprite = _fullHeart;
        }
    }

    public void UpdateHearts()
    {
        _tempHealth = _playerCurrentHealth.runtimeValue / 2;
        for (int i = 0; i < _heartContainer.initialValue; i++)
        {
            if (i <= _tempHealth - 1)
            {
                //Full heart
                _hearts[i].sprite = _fullHeart;
            }
            else if (i >= _tempHealth)
            {
                //Empty heart
                _hearts[i].sprite = _emptyHeart;
            }
            else
            {
                //half heart
                _hearts[i].sprite = _halfFullHeart;
            }
        }
    }
}
