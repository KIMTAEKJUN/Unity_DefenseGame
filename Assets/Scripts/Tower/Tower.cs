using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public string die
    public string Name { get; set; }
    public float Range { get; set; }
    public float AttackPower { get; set; }
    public float AttackSpeed { get; set; }
    
    // 생성자 추가, 속성들 초기화
    protected Tower(string name, float range, float attackPower, float attackSpeed)
    {
        Name = name;
        Range = range;
        AttackPower = attackPower;
        AttackSpeed = attackSpeed;
    }
    
    // 기본 공격 로직
    public abstract void Attack(Enemy enemy);
}
