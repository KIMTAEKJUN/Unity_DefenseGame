using Player;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private int towerBuildCost = 50;

    public void SpawnTower(Transform tileTransform)
    {
        if (towerBuildCost > playerGold.CurrentGold)
        {
            return;
        }
        
        Tile tile = tileTransform.GetComponent<Tile>();
        
        if (tile.IsBuildTower == true)
        {
            return;
        }
        
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerBuildCost;
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
        TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        towerWeapon.SetUp(enemySpawner);
    }
}