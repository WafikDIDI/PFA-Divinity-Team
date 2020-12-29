using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    // Prefabs
    [SerializeField] private Transform rangedEnemyPrefab = null;
    [SerializeField] private Transform meleeEnemyPrefab = null;

    [Space]
    [SerializeField] private List<Transform> bossSpawnPoints = new List<Transform>();

    public Transform[] SpawnEnemy (int amount)
    {
        if (amount <= 0) { return null; }

        var enemiesSpawned = new Transform[amount];

        for (int i = 0; i < amount; i++)
        {
            var spawnPointIndex = Random.Range(0, bossSpawnPoints.Count);
            Vector3 spawnPoint = bossSpawnPoints[spawnPointIndex].position;

            enemiesSpawned[i] = Instantiate(rangedEnemyPrefab, spawnPoint, Quaternion.identity);
        }

        return enemiesSpawned;
    }
}
