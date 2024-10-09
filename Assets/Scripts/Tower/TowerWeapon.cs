using System.Collections;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackRange = 2.0f;
    
    private WeaponState _weaponState = WeaponState.SearchTarget;
    private Transform _attackTarget = null;
    private EnemySpawner _enemySpawner;

    public void SetUp(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(_weaponState.ToString());
        _weaponState = newState;
        StartCoroutine(_weaponState.ToString());
    }

    private void Update()
    {
        if (_attackTarget != null)
        {
            RotateToTarget();
        }
    }
    
    private void RotateToTarget()
    {
        float dx = _attackTarget.position.x - transform.position.x;
        float dy = _attackTarget.position.y - transform.position.y;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i < _enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(_enemySpawner.EnemyList[i].transform.position, transform.position);
                if (distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    _attackTarget = _enemySpawner.EnemyList[i].transform;
                }
            }
            
            if (_attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            
            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            if (_attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            
            float distance = Vector3.Distance(_attackTarget.position, transform.position);
            if (distance > attackRange)
            {
                _attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(attackRate);

            SpawnProjectTile();
        }
    }

    private void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().SetUp(_attackTarget);
    }
}
