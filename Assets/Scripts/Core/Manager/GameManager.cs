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
        [SerializeField] private GameObject gameClearCanvas;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private TextMeshProUGUI gameClearText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Image gameClearFadeImage;
        [SerializeField] private Image gameOverFadeImage;
        [SerializeField] private float fadeDuration = 1f;

        private void Start()
        {
            playerHp.OnPlayerDeath += HandleGameOver;
            WaveManager.Instance.OnWaveEnd += HandleWaveEnd;
        }

        private void OnDestroy()
        {
            playerHp.OnPlayerDeath -= HandleGameOver;
            WaveManager.Instance.OnWaveEnd += HandleWaveEnd;
        }
        
        private void HandleWaveEnd(int waveIndex)
        {
            if (waveIndex >= WaveManager.Instance.MaxWave - 1)
            {
                HandleGameClear();
            }
        }

        private void HandleGameOver()
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;  // 게임 정지
            ShowGameOverUI();
        }
        
        private void HandleGameClear()  // 게임 클리어 시 호출
        {
            Debug.Log("Game Clear!");
            Time.timeScale = 0;  // 게임 정지
            ShowGameClearUI();
        }
        
        private void ShowGameOverUI()
        {
            gameOverCanvas.SetActive(true);
            StartCoroutine(GameOverFadeIn());
        }
        
        private void ShowGameClearUI()
        {
            gameClearCanvas.SetActive(true);
            StartCoroutine(GameClearFadeIn());
        }
        
        private IEnumerator GameOverFadeIn()
        {
            float elapsedTime = 0f;
            Color color = gameOverFadeImage.color;
            color.a = 0f;
            gameOverFadeImage.color = color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                gameOverFadeImage.color = color;
                yield return null;
            }
        }
        
        private IEnumerator GameClearFadeIn()
        {
            float elapsedTime = 0f;
            Color color = gameClearFadeImage.color;
            color.a = 0f;
            gameClearFadeImage.color = color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                gameClearFadeImage.color = color;
                yield return null;
            }
        }
    }
}