using UnityEngine;

public class Log : Enemy
{
    protected Rigidbody2D _myRigidbody;
    public Transform target;
    public Transform homePosition;
    public float chaseRadius;
    public float attackRadius;
    protected Animator _animator;
    protected Vector2 _temp;

    protected float _targetDistance;


    #region Cached Anim
    private static readonly int WakeUp = Animator.StringToHash("WakeUp");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    #endregion
    
    public void Start()
    {
        currentState = EnemyState.Idle;
        _myRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        _animator.SetBool(WakeUp,true);
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        _targetDistance = Vector2.Distance(target.position, transform.position);

        if(_targetDistance <= chaseRadius && _targetDistance >= attackRadius)
        {
            if (currentState == EnemyState.Idle ||
                currentState == EnemyState.Walk && currentState != EnemyState.Stagger)
            {
                _temp = Vector2.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
                ChangeAnim(_temp - (Vector2)transform.position);
                _myRigidbody.MovePosition(_temp);
                ChangeState(EnemyState.Walk);
                _animator.SetBool(WakeUp,true);
            }
        }
        else if(_targetDistance > chaseRadius)
        {
            _animator.SetBool(WakeUp,false);
        }
    }
    
    protected void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        _animator.SetFloat(MoveX, direction.x);
        _animator.SetFloat(MoveY, direction.y);
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
