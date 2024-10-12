using System.Collections;
using System.Collections.Generic;
using Player;
using UI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
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

    private List<Enemy> _enemyList;
    public List<Enemy> EnemyList => _enemyList;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();
            
            enemy.Setup(this, wayPoints);
            _enemyList.Add(enemy);

            SpawnEnemyHpSlider(clone);
            
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHpSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHpSliderPrefab);
        
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHp>());
    }
    
    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        if (type == EnemyDestroyType.Arrive)
        {
            playerHp.TakeDamage(1);
        }
        else if (type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }
        
        _enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
