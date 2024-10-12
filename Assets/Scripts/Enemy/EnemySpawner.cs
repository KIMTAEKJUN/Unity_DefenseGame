using System.Collections;
using System.Collections.Generic;
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
    
    public void DestroyEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
