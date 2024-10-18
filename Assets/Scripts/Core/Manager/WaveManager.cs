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

        public event Action<int> OnWaveEnd;
        public event Action<int> OnWaveStart;
        public event Action OnAllWavesCompleted;

        private bool _isWaveInProgress = false;
        public bool IsWaveInProgress => _isWaveInProgress;

        private void OnDestroy()
        {
            CancelInvoke();
        }

        public void StartWave()
        {
            if (_isWaveInProgress || _currentWaveIndex >= waves.Length - 1) return;

            _currentWaveIndex++;
            _isWaveInProgress = true;
            OnWaveStart?.Invoke(_currentWaveIndex);
            enemyManager.StartWave(waves[_currentWaveIndex]);
        }

        // 웨이브 종료 확인 및 이벤트 호출 메서드
        public void CheckWaveEnd()
        {
            if (enemyManager.EnemyList.Count == 0 && _isWaveInProgress)
            {
                _isWaveInProgress = false;
                OnWaveEnd?.Invoke(_currentWaveIndex);

                if (_currentWaveIndex < waves.Length - 1)
                {
                    Invoke(nameof(StartWave), timeBetweenWaves);
                }
                else
                {
                    OnAllWavesCompleted?.Invoke();
                }
            }
        }
    }
}