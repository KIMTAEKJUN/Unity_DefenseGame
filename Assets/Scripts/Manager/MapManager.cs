using System.Collections.Generic;
using Pattern;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manager
{
    public class MapManager : Singleton<MapManager>
    {
        public Tilemap groundTilemap;
        public Tilemap pathTilemap;
        public Tilemap towerPlacementTilemap;

        private List<Vector3> _enemyPath;
        private List<Vector3Int> _towerPlacementPositions;

        protected override void Awake()
        {
            base.Awake();
            InitializeMap();
        }

        private void InitializeMap()
        {
            _enemyPath = new List<Vector3>();
            _towerPlacementPositions = new List<Vector3Int>();

            // 적 경로 초기화
            foreach (var position in pathTilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
                if (pathTilemap.HasTile(localPlace))
                {
                    Vector3 worldPosition = pathTilemap.CellToWorld(localPlace);
                    _enemyPath.Add(worldPosition);
                }
            }

            // 정렬된 경로를 만들기 위해 필요한 추가 로직이 여기에 들어갈 수 있습니다.

            // 타워 배치 가능 위치 초기화
            foreach (var position in towerPlacementTilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
                if (towerPlacementTilemap.HasTile(localPlace))
                {
                    _towerPlacementPositions.Add(localPlace);
                }
            }
        }

        public List<Vector3> GetEnemyPath()
        {
            return new List<Vector3>(_enemyPath);
        }

        public bool CanPlaceTower(Vector3 worldPosition)
        {
            Vector3Int cellPosition = towerPlacementTilemap.WorldToCell(worldPosition);
            return _towerPlacementPositions.Contains(cellPosition);
        }

        public void PlaceTower(Vector3 worldPosition)
        {
            Vector3Int cellPosition = towerPlacementTilemap.WorldToCell(worldPosition);
            if (_towerPlacementPositions.Contains(cellPosition))
            {
                _towerPlacementPositions.Remove(cellPosition);
                // 여기에 타워 생성 로직 추가
                TowerManager.Instance.SpawnTower(0, worldPosition); // 0은 기본 타워 인덱스
            }
        }

        public void RemoveTower(Vector3 worldPosition)
        {
            Vector3Int cellPosition = towerPlacementTilemap.WorldToCell(worldPosition);
            if (!_towerPlacementPositions.Contains(cellPosition))
            {
                _towerPlacementPositions.Add(cellPosition);
                // 여기에 타워 제거 로직 추가
            }
        }
    }
}