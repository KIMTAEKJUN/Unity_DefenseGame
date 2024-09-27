using Pattern;
using UnityEngine;

namespace Manager
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        public float Money { get; private set; } = 0;

        public void AddMoney(float amount)
        {
            Money += amount;
            Debug.Log($"돈 {amount}을 획득했습니다. 현재 돈: {Money}");
        }
        
        public bool SpendMoney(float amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                Debug.Log($"돈이 {amount}만큼 차감되었습니다. 남은 돈: {Money}");
                return true; // 돈 차감 성공
            }
            Debug.Log("돈이 부족하여 차감할 수 없습니다.");
            return false; // 돈 차감 실패
        }
    }
}