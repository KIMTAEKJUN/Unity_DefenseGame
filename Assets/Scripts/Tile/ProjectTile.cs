using System;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private Movement _movement;
    private Transform _target;
    private int _damage;

    public void SetUp(Transform target, int damage)
    {
        _movement = GetComponent<Movement>();
        _target = target;
        _damage = damage;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _movement.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if (collision.transform != _target) return;
        
        collision.GetComponent<EnemyHp>().TakeDamage(_damage);
        Destroy(gameObject);
    }
}