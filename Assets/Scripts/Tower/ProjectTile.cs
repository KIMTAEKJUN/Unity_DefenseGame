using System;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private Movement _movement;
    private Transform _target;

    public void SetUp(Transform target)
    {
        _movement = GetComponent<Movement>();
        _target = target;
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
        
        collision.GetComponent<Enemy>().OnDie();
        Destroy(gameObject);
    }
}