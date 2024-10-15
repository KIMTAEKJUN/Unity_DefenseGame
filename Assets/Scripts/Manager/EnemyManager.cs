using System.Collections;
using System.Collections.Generic;
using Pattern;
using Player;
using UI;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField] 
        private GameObject enemyHpSliderPrefab;

        [SerializeField] private Transform canvasTransform;
        [SerializeField] private float spawnTime;
        [SerializeField] private Transform[] wayPoints;
        [SerializeField] private PlayerHp playerHp;
        [SerializeField] private PlayerGold playerGold;
        
        private List<Enemy> _enemyList = new List<Enemy>();
        public List<Enemy> EnemyList => _enemyList;

        private void Start()
        {
            StartCoroutine(SpawnEnemyRoutine());
        }
        
        private IEnumerator SpawnEnemyRoutine()
        {
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnTime);
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemyObject = Instantiate(enemyPrefab);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
        
            enemy.Setup(this, wayPoints);
            _enemyList.Add(enemy);

            CreateEnemyHpSlider(enemyObject);
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
        
            _enemyList.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }
}