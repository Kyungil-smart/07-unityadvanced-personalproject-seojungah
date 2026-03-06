using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject monsterPrefab;
        [SerializeField] private int maxMonsters = 10;
        [SerializeField] private float respawnDelay = 3f;
        
        [Header("Spawn Points")]
        [SerializeField] private Transform[] spawnPoints;

        private List<GameObject> _activeMonsters = new List<GameObject>();
        private float _spawnTimer = 0f;

        void Start()
        {
            for (int i = 0; i < maxMonsters; i++)
            {
                SpawnMonster();
            }
        }

        void Update()
        {
            _activeMonsters.RemoveAll(monster => monster == null);

            if (_activeMonsters.Count < maxMonsters)
            {
                _spawnTimer += Time.deltaTime;

                // 쿨타임 확인
                if (_spawnTimer >= respawnDelay)
                {
                    SpawnMonster();
                    _spawnTimer = 0f;
                }
            }
            else
            {
                _spawnTimer = 0f;
            }
        }

        private void SpawnMonster()
        {
            if (spawnPoints.Length == 0 || monsterPrefab == null) return;

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            GameObject newMonster = Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
            _activeMonsters.Add(newMonster);
        }
    }
}