// Spawners.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class Spawners : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int round;
    public GameObject[] spawnPoints;
    public GameObject[] enemyPrefab;
    public GameObject portal;
    public bool isSpawning;

    void Update()
    {
        if (isSpawning == true)
        {
            if (enemiesAlive == 0)
            {
                round++;
                NextWave();
            }

            if (round >= 5)
            {
                enemiesAlive = 100;
                portal.SetActive(true);
            }
        }
    }

    public void NextWave()
    {
        for (int i = 0; i < round + Random.Range(1, 8); i++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn enemy
            Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], spawnPoint.transform.position, Quaternion.identity);

            enemiesAlive++;
            if (round >= 4)
            {
                break;
            }
        }
    }

    public void DefeatedEnemy()
    {
        enemiesAlive--;
    }
}
