using System.Collections.Generic;
using UnityEngine;
using Manager;

public abstract class Enemy : MonoBehaviour {
    public string Name { get; private set; }
    public float Health { get; private set; }
    public float Speed { get; private set; }
    public Vector3 Position { 
        get => transform.position;
        set => transform.position = value;
    }
    public List<Vector3> Path { get; private set; }
    private int _currentPathIndex;
    
    public void SetName(string newEnemyName)
    {
        Name = newEnemyName;
    }
    
    protected virtual void Awake()
    {
        Path = new List<Vector3>();
        _currentPathIndex = 0;
    }
    
    public virtual void Move()
    {
        if (_currentPathIndex < Path.Count)
        {
            Vector3 targetPosition = Path[_currentPathIndex];
            Vector3 moveDir = (targetPosition - Position).normalized;
            Position += moveDir * Speed * Time.deltaTime;
            
            // 캐릭터 회전
            transform.rotation = Quaternion.LookRotation(moveDir);
            
            // 목적지에 도달했을 때 다음 인덱스로 이동
            if (Vector3.Distance(Position, targetPosition) < 0.1f)
            {
                _currentPathIndex++;
            }
        }
        else
        {
            ReachedEnd();
        }
        Debug.Log($"{Name}이 이동합니다. 스피드: {Speed}");
    }
    
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log($"{Name}이 {damage}의 데미지를 입었습니다. 남은 체력: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        // 적 제거 시 점수 추가
        ScoreManager.Instance.AddScore(10f);
        
        // 돈 드롭
        MoneyManager.Instance.AddMoney(50f);
        
        // 적 제거
        EnemyManager.Instance.RemoveEnemy(gameObject);
        
        Debug.Log($"{Name}이 사망했습니다.");
    }
    
    protected virtual void ReachedEnd()
    {
        // 플레이어 체력 감소
        HealthManager.Instance.ReducePlayerHealth(10f);
        
        // 적 제거
        EnemyManager.Instance.RemoveEnemy(gameObject);
        
        // 게임 오버 체크
        GameManager.Instance.CheckGameOver();
        
        Debug.Log($"{Name}이 목적지에 도달했습니다.");
    }
    
    public void SetPath(List<Vector3> newPath)
    {
        // 경로 설정
        Path = new List<Vector3>(newPath);
        _currentPathIndex = 0;
    }
    
    public virtual void SpecialAbility() 
    {
        // 적군별 특수 능력 구현
    }
    
    protected virtual void Update()
    {
        Move();
    }
}