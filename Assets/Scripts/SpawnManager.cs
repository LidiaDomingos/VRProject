using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject winScreen;
    public GameObject loseScreen;
    public PlayerLogic player;
    public Transform[] spawnPoints;
    public TextMeshProUGUI WaveCount;
    public TextMeshProUGUI ScoreCount;
    public SceneTransitionManager sceneScript;

    private int totalWaves = 5; 
    private int baseEnemiesPerWave = 2; 
    private int enemiesPerWave; 
    private int currentWave = 1;
    private int enemiesSpawned = 0;
    private int enemiesDefeated = 0;

    private bool End = false;
    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        WaveCount.text = "WAVE: " + (currentWave - 1).ToString();
        ScoreCount.text = "SCORE: " + (player.score).ToString();

        if (!End){
            if (enemiesDefeated >= enemiesPerWave)
            {
                if (currentWave <= totalWaves)
                {
                    StartNextWave();
                }
                else {
                    if (!End){
                        End = true;
                        winScreen.SetActive(true);
                    }
                }
            }
        }

        if (player.isPlayerDead & !End){
            loseScreen.SetActive(true);
            End = true;
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
        enemiesDefeated = enemiesDefeated + 1;
    }
}
