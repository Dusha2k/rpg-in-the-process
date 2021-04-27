using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger,
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    [SerializeField]protected FloatValue _maxHealth;
    [SerializeField]protected float _health;
    [SerializeField]protected string _nameEnemy;
    [SerializeField]protected float _moveSpeed;
    [SerializeField]protected int _baseAttack;


    public void Awake()
    {
        _health = _maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }
    
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.Walk;
        }
    }
}
