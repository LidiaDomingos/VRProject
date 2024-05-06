using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public TextMeshProUGUI WaveCount;

    public int totalWaves = 5; 
    private int baseEnemiesPerWave = 2; 
    private int enemiesPerWave; 
    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private int enemiesDefeated = 0;

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        WaveCount.text = (currentWave).ToString();

        if (enemiesDefeated >= enemiesPerWave)
        {
            if (currentWave <= totalWaves)
            {
                StartNextWave();
            }
            else
            {
                Debug.Log("All waves completed!");
               
            }
        }
    }

    void StartNextWave()
    {
        currentWave = currentWave + 1;
        enemiesPerWave = baseEnemiesPerWave * currentWave;
        enemiesSpawned = 0;
        enemiesDefeated = 0;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {

        while (enemiesSpawned != enemiesPerWave){
            SpawnRandomEnemy(spawnPoints[Random.Range(0, spawnPoints.Length)]);
            enemiesSpawned=enemiesSpawned+ 1;
            yield return new WaitForSeconds(1.0f); 
        }
        // for (int i = 0; i < spawnPoints.Length; i++)
        // {
        //     for (int j = 0; j < enemiesPerWave / spawnPoints.Length; j++)
        //     {
        //         SpawnRandomEnemy(spawnPoints[i]);
        //         enemiesSpawned=enemiesSpawned+ 1;
        //         yield return new WaitForSeconds(1.0f); 
        //     }
        // }
    }

    void SpawnRandomEnemy(Transform spawnPoint)
    {
        if (enemiesSpawned < enemiesPerWave)
        {
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated= enemiesDefeated+1;
    }
}
