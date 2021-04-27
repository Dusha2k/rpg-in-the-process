using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected Signal _changeSignal;
    [SerializeField]protected bool _playerInRange;

    protected virtual void Start()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _changeSignal.Raise();
            _playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _changeSignal.Raise();
            _playerInRange = false;
        }
    }
}
