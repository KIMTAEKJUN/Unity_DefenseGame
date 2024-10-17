using System;
using Map;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class WaveManager : Singleton<WaveManager>
    {
        [SerializeField] private Wave[] waves;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private float timeBetweenWaves = 5f;
        private int _currentWaveIndex = -1;
        
        public int CurrentWave => _currentWaveIndex + 1;
        public int MaxWave => waves.Length;

        // 웨이브 종료 이벤트
        public event Action<int> OnWaveEnd;
        public event Action<int> OnWaveStart;
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void StartWave()
        {
            if (_currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;
                OnWaveStart?.Invoke(_currentWaveIndex);
                enemyManager.StartWave(waves[_currentWaveIndex]);
            }
        }

        // 웨이브 종료 확인 및 이벤트 호출 메서드
        public void CheckWaveEnd()
        {
            if (enemyManager.EnemyList.Count == 0)
            {
                OnWaveEnd?.Invoke(_currentWaveIndex);
                if (_currentWaveIndex < waves.Length - 1)
                {
                    // 다음 웨이브를 timeBetweenWaves 초 후에 시작
                    Invoke(nameof(StartWave), timeBetweenWaves);
                }
            }
        }
        
        public void OnDestroy()
        {
            CancelInvoke();
        }
    }
}