using System.Collections.Generic;
using Pattern;
using UnityEngine;

namespace Manager
{
    public class TowerManager : Singleton<TowerManager>
    {
        public List<GameObject> towerPrefabs;
        private int _towerCounter = 0;
        private int _selectedTowerIndex = 0;
        private bool _isPlacingTower = false;
    
        private void Update()
        {
            if (_isPlacingTower && Input.GetMouseButtonDown(0)) // 좌클릭
            {
                PlaceTower();
            }
        }

        // 타워 배치 모드를 시작하는 메서드
        public void StartPlacingTower(int towerIndex)
        {
            _selectedTowerIndex = towerIndex;
            _isPlacingTower = true;
        }

        // 타워를 배치하는 메서드
        private void PlaceTower()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            if (hit.collider != null)
            {
                Vector3 towerPosition = hit.point;
                if (MapManager.Instance.CanPlaceTower(towerPosition))
                {
                    SpawnTower(_selectedTowerIndex, towerPosition);
                    _isPlacingTower = false;
                }
            }
        }
        
        // 타워를 생성하는 메서드
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