using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using Pattern;
using Player;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField] 
        private GameObject enemyHpSliderPrefab;
        
        [SerializeField] private Transform canvasTransform;
        [SerializeField] private Transform[] wayPoints;
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private PlayerGold playerGold;
        
        private WaveManager _waveManager;
        private Wave _currentWave;
        private int _currentEnemyCount;
        private List<Enemy> _enemyList = new List<Enemy>();
        
        public List<Enemy> EnemyList => _enemyList;
        public int CurrentEnemyCount => _currentEnemyCount;
        public int MaxEnemyCount => _currentWave.maxEnemyCount;

        private void Start()
        {
            _waveManager = GetComponent<WaveManager>();
            if (_waveManager == null)
            {
                Debug.LogError("웨이브 시스템이 없습니다.");
            }
        }

        public void StartWave(Wave wave)
        {
            _currentWave = wave;
            _currentEnemyCount = _currentWave.maxEnemyCount;
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            int spawnEnemyCount = 0;

            while (spawnEnemyCount < _currentWave.maxEnemyCount)
            {
                int enemyIndex = Random.Range(0, _currentWave.enemyPrefabs.Length);
                GameObject enemyObject = Instantiate(_currentWave.enemyPrefabs[enemyIndex]);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                
                enemy.Setup(this, wayPoints);
                _enemyList.Add(enemy);

                spawnEnemyCount++;
                
                CreateEnemyHpSlider(enemyObject);
                
                yield return new WaitForSeconds(_currentWave.spawnTime);
            }
        }

        private void CreateEnemyHpSlider(GameObject enemyObject)
        {
            GameObject sliderObject = Instantiate(enemyHpSliderPrefab, canvasTransform);
            sliderObject.transform.localScale = Vector3.one;
        
            SliderPositionAutoSetter sliderPositionSetter = sliderObject.GetComponent<SliderPositionAutoSetter>();
            sliderPositionSetter.Setup(enemyObject.transform);

            EnemyHpViewer enemyHpViewer = sliderObject.GetComponent<EnemyHpViewer>();
            enemyHpViewer.Setup(enemyObject.GetComponent<EnemyHp>());
        }
    
        public void HandleEnemyDestroy(EnemyDestroyType type, Enemy enemy, int gold)
        {
            switch (type)
            {
                case EnemyDestroyType.Arrive:
                    playerHp.TakeDamage(1);
                    break;
                case EnemyDestroyType.Kill:
                    playerGold.CurrentGold += gold;
                    break;
            }

            _currentEnemyCount--;
            _enemyList.Remove(enemy);
            Destroy(enemy.gameObject);
            
            // 모든 적이 제거되면 다음 웨이브 시작
            if (_enemyList.Count == 0)
            {
                _waveManager.StartWave();
            }
        }
    }
}