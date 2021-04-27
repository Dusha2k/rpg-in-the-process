using UnityEngine;
using UnityEngine.UI;


public class TreasureChest : Interactable
{
    [SerializeField] private Item _contentItem;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Signal _raiseItem;
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private Text _dialogText;
    
    public bool _isOpen;
    private Animator _anim;
    
    protected override void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && _playerInRange)
        {
            if (!_isOpen)
            {
                OpenChest();
            }
            else
            {
                CheastAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        _dialogBox.SetActive(true);
        _dialogText.text = _contentItem.descriptionItem;
        _playerInventory.AddItem(_contentItem);
        _playerInventory.currentItem = _contentItem;
        //Raise the signal to the player to animate
        _raiseItem.Raise();
        //Raise the changeContext
        _changeSignal.Raise();
        _anim.SetBool("opened",true);
        _isOpen = true;
    }

    public void CheastAlreadyOpen()
    {
        _dialogBox.SetActive(false);
        _raiseItem.Raise();
        _playerInRange = false;
        Debug.Log("Try to open chest");
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_isOpen)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !_isOpen)
        {
            base.OnTriggerExit2D(other);
        }
    }
}
