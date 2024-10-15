using Map;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class WaveManager : Singleton<WaveManager>
    {
        [SerializeField] private Wave[] waves;
        [SerializeField] private EnemyManager enemyManager;
        private int _currentWaveIndex = -1;
        
        public int CurrentWave => _currentWaveIndex + 1;
        public int MaxWave => waves.Length;
        
        public void StartWave()
        {
            if (enemyManager.EnemyList.Count == 0 && _currentWaveIndex < waves.Length - 1)
            {
                _currentWaveIndex++;
                enemyManager.StartWave(waves[_currentWaveIndex]);
            }
        }
    }
}