using System.Collections;
using Pattern;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 1f;

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
            StartCoroutine(FadeIn());
        }
        
        private IEnumerator FadeIn()
        {
            float elapsedTime = 0f;
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }
    }
}