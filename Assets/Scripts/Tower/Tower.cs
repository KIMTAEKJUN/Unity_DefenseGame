using UnityEngine;

public abstract class Tower : MonoBehaviour {
    public string Name { get; set; }
    public float Range { get; set; }
    public float AttackPower { get; set; }
    public float AttackSpeed { get; set; }
    public int Level { get; set; }
    
    public void SetName(string newTowerName)
    {
        Name = newTowerName;
    }

    protected virtual void Awake()
    {
        Range = 1f;
        AttackPower = 1f;
        AttackSpeed = 1f;
        Level = 1;
    }

    // 기본 공격 로직
    public abstract void Attack(Enemy enemy);
    
    public virtual void SpecialAbility() 
    {
        // 타워별 특수 능력 구현
        
    }
}  
