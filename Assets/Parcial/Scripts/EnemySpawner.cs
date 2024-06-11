using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 5f;

    private void Start()
    {
        // Puedes usar esto para verificar si los eventos se invocan correctamente al iniciar el juego.
        StartSpawning();
    }

    void OnEnable()
    {
        Debug.Log("EnemySpawner enabled");
        EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

    void OnDisable()
    {
        Debug.Log("EnemySpawner disabled");
        StopSpawning();
        EventManager.onStartGame -= StartSpawning;
        EventManager.onPlayerDeath -= StopSpawning;
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawning enemy");
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void StartSpawning()
    {
        Debug.Log("Starting to spawn enemies");
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
    }

    void StopSpawning()
    {
        Debug.Log("Stopping spawning enemies");
        CancelInvoke("SpawnEnemy");
    }
}
