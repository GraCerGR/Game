using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenerateEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform spawnPointsParent;
    private List<Transform> spawnPoints = new List<Transform>();
    private int enemyCount;
    public int enemyCountMax = 5;
    public int enemyCountMaxInScene = 30;
    public float timeSpawn = 60f;

    void Start()
    {
        foreach (Transform child in spawnPointsParent)
        {
            spawnPoints.Add(child);
        }

        StartCoroutine(StartSpawnEnemy());

        StartCoroutine(SpawnEnemyEveryMinute());
    }

    IEnumerator StartSpawnEnemy()
    {
        while (enemyCount < enemyCountMax)
        {
            SpawnEnemy();
            enemyCount++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SpawnEnemyEveryMinute()
    {
        while (true)
        {
            int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (currentEnemyCount < enemyCountMaxInScene)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(timeSpawn);
        }
    }

    void SpawnEnemy()
    {
        int randIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randIndex];

        int randEnemyIndex = Random.Range(0, enemies.Length);
        GameObject selectedEnemy = enemies[randEnemyIndex];

        Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);
    }
}
