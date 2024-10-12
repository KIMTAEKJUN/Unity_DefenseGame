using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _wayPointCount;
    private Transform[] _wayPoints;
    private int _currentWayPointIndex = 0;
    private Movement _movement;
    private EnemySpawner _enemySpawner;

    [SerializeField] private int gold = 10;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        _movement = GetComponent<Movement>();
        _enemySpawner = enemySpawner;
        
        _wayPointCount = wayPoints.Length;
        _wayPoints = new Transform[_wayPointCount];
        _wayPoints = wayPoints;
        
        transform.position = _wayPoints[_currentWayPointIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();
        
        while (true)
        {
            transform.Rotate(Vector3.forward * 10);
            
            if (Vector3.Distance(transform.position, _wayPoints[_currentWayPointIndex].position) < 0.02f * _movement.MoveSpeed)
            {
                NextMoveTo();
            }
            
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (_currentWayPointIndex < _wayPointCount - 1)
        {
            transform.position = _wayPoints[_currentWayPointIndex].position;
            _currentWayPointIndex++;
            Vector3 direction = (_wayPoints[_currentWayPointIndex].position - transform.position).normalized;
            _movement.MoveTo(direction);
        }
        else
        {
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        _enemySpawner.DestroyEnemy(type, this, gold);
    }
}