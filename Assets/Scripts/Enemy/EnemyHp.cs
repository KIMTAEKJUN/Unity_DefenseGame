using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxHp;
    private float _currentHp;
    private bool _isDie = false;
    private Enemy _enemy;
    private SpriteRenderer _spriteRenderer;
    
    public float MaxHp => maxHp;
    public float CurrentHp => _currentHp;
    
    private void Awake()
    {
        _currentHp = maxHp;
        _enemy = GetComponent<Enemy>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (_isDie == true) return;
        
        _currentHp -= damage;
        
        if (_currentHp <= 0)
        {
            _isDie = true;
            _enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = _spriteRenderer.color;

        color.a = 0.4f;
        _spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        _spriteRenderer.color = color;
    }
}