using Pattern;
using Player;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private TextMeshProUGUI gameOverText;

        private void Start()
        {
            playerHp.OnPlayerDeath += HandleGameOver;
            gameOverCanvas.SetActive(false);  // 시작 시 게임 오버 UI 숨기기
        }

        private void OnDestroy()
        {
            playerHp.OnPlayerDeath -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;  // 게임 정지
            ShowGameOverUI();
        }
        
        private void ShowGameOverUI()
        {
            gameOverCanvas.SetActive(true);
        }
    }
}