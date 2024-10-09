using Pattern;
using UnityEngine;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentGameState { get; private set; }

        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            GameOver,
            LevelComplete
        }

        private void Start()
        {
            // Initialize game
            CurrentGameState = GameState.MainMenu;
        }

        private void Update()
        {
            if (CurrentGameState == GameState.Playing)
            {
                LevelManager.Instance.UpdateLevel(Time.deltaTime);
            }
        }

        public void StartGame(int level)
        {
            HealthManager.Instance.Initialize(100f);
            CurrentGameState = GameState.Playing;
            LevelManager.Instance.StartLevel(level);
        }

        public void PauseGame()
        {
            if (CurrentGameState == GameState.Playing)
            {
                CurrentGameState = GameState.Paused;
                Time.timeScale = 0f;
            }
            else if (CurrentGameState == GameState.Paused)
            {
                CurrentGameState = GameState.Playing;
                Time.timeScale = 1f;
            }
        }

        public void GameOver()
        {
            CurrentGameState = GameState.GameOver;
            // Handle game over logic
        }

        public void OnLevelComplete()
        {
            CurrentGameState = GameState.LevelComplete;
            // Handle level completion logic
        }

        public void CheckGameOver()
        {
            if (HealthManager.Instance.PlayerHealth <= 0)
            {
                GameOver();
            }
        }
    }
}