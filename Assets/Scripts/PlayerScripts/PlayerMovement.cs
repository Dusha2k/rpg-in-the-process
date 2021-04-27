using System;
using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Attack,
    Interact,
    Idle,
    Stagger,
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    [SerializeField] private float _speed;
    [SerializeField] private FloatValue _currentHealth;
    [SerializeField] private VectorValue _startingPosition;
    [SerializeField] private Signal _playerHealthSignal;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private SpriteRenderer _receiveItemSprite;
    
    private Rigidbody2D _myRigidbody;
    private Vector3 _change;
    private Animator _animator;

    #region CachedAnim
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    #endregion
    

    void Start()
    {
        _animator = GetComponent<Animator>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _animator.SetFloat(MoveX, 0);
        _animator.SetFloat(MoveY, -1);
        transform.position = _startingPosition.initialValue;
    }

    private void Update()
    {
        if (currentState == PlayerState.Interact)
            return;
        _change.x = Input.GetAxisRaw("Horizontal");
        _change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.Attack && currentState != PlayerState.Stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.Walk || currentState == PlayerState.Idle)
        {
            UpdateAnimationAndMove();
        }
   }

    private IEnumerator AttackCo()
    {
        _animator.SetBool(IsAttacking, true);
        currentState = PlayerState.Attack;
        yield return null;
        _animator.SetBool(IsAttacking, false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.Interact)
        {
            currentState = PlayerState.Walk;
        }
    }

    void UpdateAnimationAndMove()
    {
        if (_change != Vector3.zero)
        {
            MoveCharacter();
            _animator.SetFloat(MoveX, _change.x);
            _animator.SetFloat(MoveY, _change.y);
            _animator.SetBool(IsWalking, true);
        }
        else
        {
            _animator.SetBool(IsWalking, false);
        }
    }

    private void MoveCharacter()
    {
        _change.Normalize();
        _myRigidbody.MovePosition(transform.position + _change * (_speed * Time.fixedDeltaTime));
    }

    public void Knock(float knockTime,float damage)
    {
        _currentHealth.runtimeValue -= damage;
        _playerHealthSignal.Raise();
        if (_currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    public void RaiseItem()
    {
        if (_playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.Interact)
            {
                _animator.SetBool("receiveItem",true);
                currentState = PlayerState.Interact;
                _receiveItemSprite.sprite = _playerInventory.currentItem.itemSprite;
            }
            else
            {
                _animator.SetBool("receiveItem", false);
                currentState = PlayerState.Idle;
                _receiveItemSprite.sprite = null;
                _playerInventory.currentItem = null;
            }
        }
    }
    
    private IEnumerator KnockCo(float knockTime)
    {
        if (_myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            _myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.Idle;
        }
    }
}
