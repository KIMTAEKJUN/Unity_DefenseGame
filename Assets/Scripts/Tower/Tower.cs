using UnityEngine;

public abstract class Tower : MonoBehaviour {
    public string Name { get; set; }
    public float Range { get; set; }
    public float AttackPower { get; set; }
    public float AttackSpeed { get; set; }
    public int Level { get; set; }

    // 생성자 추가, 속성들 초기화
    protected Tower(string name, float range, float attackPower, float attackSpeed)
    {
        Name = name;
        Range = range;
        AttackPower = attackPower;
        AttackSpeed = attackSpeed;
        Level = 1;
    }

    // 기본 공격 로직
    public abstract void Attack(Enemy enemy);
    
    public virtual void Upgrade()
    {
        Level++;
        AttackPower *= 1.2f;
        Range *= 1.1f;
        Debug.Log($"{Name}이 업그레이드되었습니다. 레벨: {Level}");
    }
    
    public virtual void SpecialAbility() 
    {
        // 타워별 특수 능력 구현
        
    }
}  
