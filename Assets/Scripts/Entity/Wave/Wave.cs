using System;
using UnityEngine;

namespace Map
{
    // 구조체, 클래스를 직렬화함. 메모리에 존재하는 오브젝트 정보를 string, byte로 변환하는 것
    [Serializable]
    public struct Wave
    {
        public float spawnTime;
        public int maxEnemyCount;
        public GameObject[] enemyPrefabs;
    }
}