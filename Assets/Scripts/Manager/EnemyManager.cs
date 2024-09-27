using System.Collections.Generic;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public List<GameObject> enemyPrefabs;
        public float spawnInterval = 5f;
        private int _enemyCounter = 0;
        
        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
        }
        
        public void SpawnEnemy()
        {
            if (enemyPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                GameObject enemyObject = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                Debug.Log($"적이 {spawnPosition}에 생성되었습니다.");
                
                if (enemy != null)
                {
                    _enemyCounter++;
                    string enemyName = $"Enemy_{_enemyCounter}";
                    enemy.SetName(enemyName);
                    Debug.Log($"{enemyName}이 {spawnPosition}에 생성되었습니다.");
                }
                else
                {
                    Debug.LogWarning("적 프리팹에 Enemy 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("적 프리팹이 설정되지 않았습니다.");
            }
        }

        // 적을 제거하는 메서드
        public void RemoveEnemy(GameObject enemy)
        {
            Destroy(enemy);
            Debug.Log("적이 제거되었습니다.");
        }
    }
}