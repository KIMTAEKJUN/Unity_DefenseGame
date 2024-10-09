using UnityEngine;

public abstract class Tower : MonoBehaviour {
    public string Name { get; set; }
    public float Range { get; protected set; }
    public float AttackPower { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public int Level { get; protected set; }
    protected float LastAttackTime { get; set; }
    
    // 타워 이름을 설정하는 메서드
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
        LastAttackTime = 0f;
    }

    // 적을 공격하는 추상 메서드
    public abstract void Attack(Enemy enemy);
    
    // 타워의 특수 능력을 구현하는 메서드
    public virtual void SpecialAbility() 
    {
        // 타워별 특수 능력 구현
    }
    
    protected virtual void Update()
    {
        if (Time.time - LastAttackTime >= 1f / AttackSpeed)
        {
            Enemy target = FindClosestEnemy();
            if (target != null)
            {
                Attack(target);
                LastAttackTime = Time.time;
            }
        }
    }

    // 가장 가까운 적을 찾는 메서드
    protected Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= Range && distance < closestDistance)
            {
                closest = enemy;
                closestDistance = distance;
            }
        }

        return closest;
    }
}  
