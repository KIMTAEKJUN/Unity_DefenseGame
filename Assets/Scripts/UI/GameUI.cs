using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        public Text moneyText;
        public Text timeText;
        public Button buildTowerButton;
        public Button pauseButton;

        private bool _isPaused = false;

        public void UpdateMoneyUI(float money)
        {
            moneyText.text = $"현재 돈: {money}";
        }

        public void UpdateTimeUI(float timeLeft)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            timeText.text = $"남은 시간: {minutes:00}:{seconds:00}";
        }

        public void BuildTower()
        {
            // 타워 건설 모드 활성화
            // 이 부분은 플레이어가 맵에서 타워를 배치할 위치를 선택할 수 있도록 구현해야 합니다.
            Debug.Log("타워 건설 모드 활성화");
        }

        public void PauseGame()
        {
            GameManager.Instance.PauseGame();
            _isPaused = !_isPaused;
            pauseButton.GetComponentInChildren<Text>().text = _isPaused ? "계속하기" : "일시정지";
        }
    }
}
