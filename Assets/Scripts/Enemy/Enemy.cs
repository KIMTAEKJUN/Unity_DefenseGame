using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public string Name { get; set; }
    public float Health { get; set; }
    public float Speed { get; set; }
    public Vector3 Position { get; set; }
    public List<Vector3> Path { get; set; }
    private int _currentPathIndex;
    
    // 생성자 추가, 속성 초기화
    protected Enemy(string name, float health, float speed)
    {
        Name = name;
        Health = health;
        Speed = speed;
        Path = new List<Vector3>();
        _currentPathIndex = 0;
    }
    
    // 이동 로직
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
    
    // 공격 로직
    public virtual void TakeDamage(float damage)
    {
        // 체력 감소
        Health -= damage;
        Debug.Log($"{Name}이 {damage}의 데미지를 입었습니다. 남은 체력: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }
    
    // 사망 로직
    private void Die()
    {
        // 적 제거
        GameObject.Destroy(gameObject);
        
        // 점수 추가
        
        
        // 아이템 드롭
        
        
        Debug.Log($"{Name}이 사망했습니다.");
    }
    
    // 적이 타워에 도달했을 때 실행되는 로직 
    protected virtual void ReachedEnd()
    {
        // 플레이어 체력 감소
        
        
        // 적 제거
        GameObject.Destroy(this.gameObject);
        
        // 게임 오버 체크
        
        Debug.Log($"{Name}이 목적지에 도달했습니다.");
    }
    
    // 새로운 경로로 이동하는 로직
    public void SetPath(List<Vector3> newPath)
    {
        // 경로 설정
        Path = newPath;
        _currentPathIndex = 0;
    }
    
    public virtual void SpecialAbility() 
    {
        // 적별 특수 능력 구현
        
    }
}