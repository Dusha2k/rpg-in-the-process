using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Vector2 _playerPosition;
    [SerializeField] private VectorValue _playerStorage;
    [SerializeField] private GameObject _fadeInPanel;
    [SerializeField] private GameObject _fadeOutPanel;
    [SerializeField] private float _faidWait;

    private void Awake()
    {
        if (_fadeOutPanel != null)
        {
            GameObject panel = Instantiate(_fadeOutPanel);
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _playerStorage.initialValue = _playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    IEnumerator FadeCo()
    {
        if (_fadeInPanel != null)
        {
            GameObject panel = Instantiate(_fadeInPanel);
        }

        yield return new WaitForSeconds(_faidWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
        if (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
