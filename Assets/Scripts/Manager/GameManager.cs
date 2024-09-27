using Pattern;
using UnityEngine;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public ScoreManager ScoreManager { get; private set; }
        public HealthManager HealthManager { get; private set; }
        public MoneyManager MoneyManager { get; private set; }
        public EnemyManager EnemyManager { get; private set; }
        public TowerManager TowerManager { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            // 매니저 초기화
            ScoreManager = gameObject.AddComponent<ScoreManager>();
            HealthManager = gameObject.AddComponent<HealthManager>();
            HealthManager.Initialize(100f);
            MoneyManager = gameObject.AddComponent<MoneyManager>();
            EnemyManager = gameObject.AddComponent<EnemyManager>();
            TowerManager = gameObject.AddComponent<TowerManager>();

            // 게임 시작 시
            ScoreManager.AddScore(10f);
            HealthManager.ReducePlayerHealth(10f);

            // 게임 오버 체크
            CheckGameOver();
        }
        
        public void CheckGameOver()
        {
            if (HealthManager.PlayerHealth <= 0)
            {
                Debug.Log("게임 오버!");
                GameOver();
            }
        }
        
        private void GameOver()
        {
            // 게임 오버 로직 구현
            Time.timeScale = 0; // 게임 일시 정지
            ShowGameOverUI(); // UI 표시
        }
        
        private void ShowGameOverUI()
        {
            Debug.Log("게임 오버 화면 표시");
            // UI 관련 코드 추가
        }
        
        public void BuyTower(int towerIndex, float cost, Vector3 position)
        {
            // SpendMoney 메서드를 사용하여 돈 차감 시도
            if (MoneyManager.SpendMoney(cost))
            {
                TowerManager.SpawnTower(towerIndex, position); // 타워 생성
                Debug.Log($"타워 구매 성공! 남은 돈: {MoneyManager.Instance.Money}");
            }
            else
            {
                Debug.Log("돈이 부족하여 타워를 구매할 수 없습니다.");
            }
        }
    }
}