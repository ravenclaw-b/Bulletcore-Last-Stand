using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject player;
    public int wave = 1;
    public int enemiesAlive = 0;
    public float timeUntilNextWave = 0f;
    public bool isBetweenWaves = false;

    private float waveDelay = 30f;
    private float spawnDelay = 5f; // Base delay, gets lower as waves go on
    private float lastSpawned;

    public int enemiesToSpawn = 5;
    public int enemiesSpawned = 0;
    private bool bossSpawned = false;

    private List<GameObject> aliveEnemies = new List<GameObject>();

    private Health playerHealth;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        playerHealth = player.GetComponent<Health>();
        enemySpawner = GetComponent<EnemySpawner>();
        lastSpawned = Time.time - spawnDelay;
        StartNewWave();
    }

    void Update()
    {
        if (!playerHealth.IsAlive())
        {
            SceneManager.LoadScene("Level");
            return;
        }

        aliveEnemies.RemoveAll(enemy => enemy == null);
        aliveEnemies.RemoveAll(enemy => enemy.GetComponent<Health>().IsAlive() == false);

        enemiesAlive = aliveEnemies.Count;

        if (isBetweenWaves)
        {
            timeUntilNextWave -= Time.deltaTime;
            if (timeUntilNextWave <= 0f)
            {
                StartNewWave();
            }
            playerHealth.health = Mathf.Min(playerHealth.totalHealth, playerHealth.health + 1);
            return;
        }

        // Adjust spawn delay dynamically
        spawnDelay = Mathf.Max(0.5f, 5f - wave * 0.2f);

        if (enemiesSpawned < enemiesToSpawn && Time.time - lastSpawned >= spawnDelay)
        {
            lastSpawned = Time.time;
            SpawnEnemy(false);
        }

        if (!bossSpawned && wave % 3 == 0 && enemiesSpawned == enemiesToSpawn)
        {
            SpawnEnemy(true); // Spawn boss
            bossSpawned = true;
        }

        if (enemiesAlive == 0 && enemiesSpawned == enemiesToSpawn && (!bossSpawned || (bossSpawned && enemiesAlive == 0)))
        {
            isBetweenWaves = true;
            timeUntilNextWave = waveDelay;
        }
    }

    void StartNewWave()
    {
        wave++;
        enemiesToSpawn = 5 + wave * 2;
        if (wave % 3 == 0) enemiesToSpawn -= 3; // fewer enemies in boss wave

        enemiesSpawned = 0;
        bossSpawned = false;
        isBetweenWaves = false;
        lastSpawned = Time.time - spawnDelay;
    }

    void SpawnEnemy(bool isBoss)
    {
        GameObject spawned = enemySpawner.SpawnEnemy();
        if (spawned == null) return;

        Health spawnedHealth = spawned.GetComponent<Health>();
        BasicEnemyMovement spawnedMove = spawned.GetComponent<BasicEnemyMovement>();
        BasicEnemyDamage spawnedDamage = spawned.GetComponent<BasicEnemyDamage>();

        if (isBoss)
        {
            spawned.transform.localScale *= 2f;
            spawnedHealth.totalHealth = 500 + wave * 50;
            spawnedMove.speed = 3f;
            spawnedDamage.damage = 20f + wave * 2f;
            bossSpawned = true;
        }
        else
        {
            spawnedHealth.totalHealth = 100 + wave * 20;
            spawnedMove.speed = 5f + wave * 0.5f;
            spawnedDamage.damage = 5f + wave * 1f;
            enemiesSpawned++;
        }

        spawnedHealth.health = spawnedHealth.totalHealth;
        aliveEnemies.Add(spawned);
    }
}
