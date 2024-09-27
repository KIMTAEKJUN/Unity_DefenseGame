using System.Collections.Generic;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class TowerManager : Singleton<TowerManager>
    {
        public List<GameObject> towerPrefabs;
        private int _towerCounter = 0;
        
        public void SpawnTower(int towerIndex, Vector3 position)
        {
            if (towerIndex >= 0 && towerIndex < towerPrefabs.Count)
            {
                GameObject towerObject = Instantiate(towerPrefabs[towerIndex], position, Quaternion.identity);
                Tower tower = towerObject.GetComponent<Tower>();
                Debug.Log($"타워가 {position}에 생성되었습니다.");
                
                if (tower != null)
                {
                    _towerCounter++;
                    string towerName = $"Tower_{_towerCounter}";
                    tower.SetName(towerName);
                    Debug.Log($"{towerName}이 {position}에 생성되었습니다.");
                }
                else
                {
                    Debug.LogWarning("타워 프리팹에 Tower 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("유효하지 않은 타워 인덱스입니다.");
            }
        }

        // 타워를 제거하는 메서드
        public void RemoveTower(GameObject tower)
        {
            Destroy(tower);
            Debug.Log("타워가 제거되었습니다.");
        }
    }
}