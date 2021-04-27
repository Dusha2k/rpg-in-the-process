using UnityEngine;

public class PatrolLog : Log
{
    [SerializeField] private Transform[] _path;
    [SerializeField] private int _currentPoint;
    [SerializeField] private Transform _currentGoal;
    
    
    
    protected override void CheckDistance()
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
                
                
            }
        }
        else if(_targetDistance > chaseRadius)
        {
            if (Vector3.Distance(transform.position, _path[_currentPoint].position) > 0.1f)
            {
                _temp = Vector2.MoveTowards(transform.position, _path[_currentPoint].position, _moveSpeed * Time.deltaTime);
                ChangeAnim(_temp - (Vector2)transform.position);
                _myRigidbody.MovePosition(_temp);
            }
            else
            {
                ChangeGoal();
            }
            
        }
    }

    private void ChangeGoal()
    {
        if (_currentPoint == _path.Length - 1)
        {
            _currentPoint = 0;
            _currentGoal = _path[0];
        }
        else
        {
            _currentPoint++;
            _currentGoal = _path[_currentPoint];
        }
    }
}
