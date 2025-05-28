using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPreFab;
    GameState gameState;

    private void Start()
    {
        gameState = GetComponent<GameState>();
    }

    public GameObject SpawnEnemy()
    {
        GameObject spawned = Instantiate(enemyPreFab);
        Vector3 pos = GetSpawnPos();

        spawned.transform.position = pos;
        spawned.GetComponent<BasicEnemyMovement>().target = gameState.player;

        Debug.Log("Enemy spawned at " + pos);

        return spawned;
    }

    Vector3 GetSpawnPos()
    {
        bool isValid = false;

        while (!isValid)
        {
            int x = Random.Range(-45, 45);
            int z = Random.Range(-45, 45);
            Vector3 spawnPos = new Vector3(x, 1, z);

            if (Physics.CheckCapsule(spawnPos, new Vector3(x, 2, z), 1) && (20 < x || x < -20) && ((20 < z || z < -20)))
            {
                return spawnPos;
            }
        }
        return new Vector3(0, 2, 0);
    }
}
