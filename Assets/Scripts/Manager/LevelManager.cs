using System.Collections.Generic;
using Pattern;

namespace Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        public int CurrentLevel { get; private set; }
        public float LevelDuration { get; private set; }
        public float ElapsedTime { get; private set; }

        private Dictionary<int, float> levelDurations = new Dictionary<int, float>
        {
            {1, 60f},
            {2, 90f},
            {3, 120f},
            // 더 많은 레벨 추가
        };

        public void StartLevel(int level)
        {
            CurrentLevel = level;
            LevelDuration = levelDurations[level];
            ElapsedTime = 0f;
            // 레벨 시작 시 처리할 내용
        }

        public void UpdateLevel(float deltaTime)
        {
            ElapsedTime += deltaTime;
            if (ElapsedTime >= LevelDuration)
            {
                CompleteLevel();
            }
        }

        private void CompleteLevel()
        {
            // 레벨 완료 시 처리할 내용
            GameManager.Instance.OnLevelComplete();
        }
    }
}