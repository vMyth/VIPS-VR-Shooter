using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    Collider trigger;

    public static bool enemiesActive = false;

    private void Start()
    {
        trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnEnemies();
            enemiesActive = true;
            trigger.enabled = false;
        }
    }

    private void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
