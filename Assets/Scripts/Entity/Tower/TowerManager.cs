using System;
using Pattern;
using Player;
using UnityEngine;

namespace Manager
{
    public class TowerManager : Singleton<TowerManager>
    {
        [SerializeField] private GameObject[] availableTowers;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerGold playerGold;
        [SerializeField] private int[] towerCosts;

        private int _selectedTowerIndex = -1;
        
        public event Action<int> OnTowerSelected;
        public bool CanBuildTower => _selectedTowerIndex != -1 && playerGold.CurrentGold >= towerCosts[_selectedTowerIndex];

        public void BuildTower(Transform tileTransform)
        {
            if (!CanBuildTower || _selectedTowerIndex == -1)
            {
                return;
            }
            
            Tile tile = tileTransform.GetComponent<Tile>();
            
            if (tile.IsBuildTower)
            {
                return;
            }
            
            tile.IsBuildTower = true;
            playerGold.CurrentGold -= towerCosts[_selectedTowerIndex];
            GameObject clone = Instantiate(availableTowers[_selectedTowerIndex], tileTransform.position, Quaternion.identity);
            TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
            towerWeapon.SetUp(enemyManager);
        }
        
        public void SelectTower(int index)
        {
            if (index >= 0 && index < availableTowers.Length)
            {
                _selectedTowerIndex = index;
                OnTowerSelected?.Invoke(index);
            }
        }
    }
}