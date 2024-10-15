using Pattern;
using Player;
using UnityEngine;

namespace Manager
{
    public class TowerManager : Singleton<EnemyManager>
    {
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerGold playerGold;
        [SerializeField] private int towerBuildCost = 50;
        
        public bool CanBuildTower => playerGold.CurrentGold >= towerBuildCost;

        public void BuildTower(Transform tileTransform)
        {
            if (!CanBuildTower)
            {
                return;
            }
            
            Tile tile = tileTransform.GetComponent<Tile>();
            
            if (tile.IsBuildTower)
            {
                return;
            }
            
            tile.IsBuildTower = true;
            playerGold.CurrentGold -= towerBuildCost;
            GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
            TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
            towerWeapon.SetUp(enemyManager);
        }
    }
}