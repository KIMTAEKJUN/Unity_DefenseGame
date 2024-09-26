using Pattern;
using UnityEngine;

namespace Manager
{
    public class GameManager : Singleton<MonoBehaviour>
    {
        public float PlayerHealth { get; private set; }
        public float Score { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            // 게임 시작 시
            Initialize(100f);
            
            // 점수 추가
            AddScore(10f);
            
            // 체력 감소
            ReducePlayerHealth(10f);
            
            // 아이템 생성
            SpawnItem(new Vector3(1, 0, 1));
            
            // 게임 오버 체크
            CheckGameOver();
        }
        
        public void Initialize(float initialPlayerHealth)
        {
            PlayerHealth = initialPlayerHealth;
            Score = 0;
            Debug.Log($"게임 매니저 초기화. 초기 체력: {PlayerHealth}");
        }
        
        public void AddScore(float amount)
        {
            Score += amount;
            Debug.Log($"점수 {amount}를 획득했습니다. 현재 점수: {Score}");
        }

        public void ReducePlayerHealth(float amount = 1)
        {
            PlayerHealth -= amount;
            Debug.Log($"데미지 {amount}를 입었습니다. 남은 체력: {PlayerHealth}");
            CheckGameOver();
        }

        public void CheckGameOver()
        {
            if (PlayerHealth <= 0)
            {
                Debug.Log("게임 오버!");
                // 게임 오버 로직 구현
            }
        }

        public void SpawnItem(Vector3 position)
        {
            // 아이템 생성 로직 구현
            Debug.Log($"아이템 생성: {position}");
        }
    }
}