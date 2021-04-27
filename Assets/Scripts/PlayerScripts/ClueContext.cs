using UnityEngine;

public class ClueContext : MonoBehaviour
{
    [SerializeField] private GameObject _clueContext;
    private bool _isActive = false;

    public void ChangeActive()
    {
        _isActive = !_isActive;
        if (_isActive)
            _clueContext.SetActive(true);
        else
            _clueContext.SetActive(false);
    }
}
