using Pattern;
using UnityEngine;

namespace Manager
{
    public class HealthManager : Singleton<HealthManager>
    {
        public float PlayerHealth { get; private set; }

        public virtual void Initialize(float initialHealth)
        {
            PlayerHealth = initialHealth;
            Debug.Log($"플레이어 초기 체력: {PlayerHealth}");
        }

        public void ReducePlayerHealth(float amount = 1)
        {
            PlayerHealth -= amount;
            Debug.Log($"데미지 {amount}를 입었습니다. 남은 체력: {PlayerHealth}");
        }
    }
}