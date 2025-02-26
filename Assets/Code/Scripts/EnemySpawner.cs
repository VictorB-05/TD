using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 2f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [SerializeField] private int currentWave = 0;
    private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] public int enemiesDead;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private bool waveStarted = false;  // Nueva variable para controlar si la ola ha comenzado

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        LevelManager.main.onWaveStart.AddListener(StartWave);
    }

    private void Update()
    {
        if (!isSpawning || !waveStarted) return;  // Solo ejecutar si la ola ha comenzado

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            Debug.Log("QUE?");
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void StartWave()
    {
        waveStarted = true;  // Marcar que la ola ha comenzado
        isSpawning = true;
        enemiesLeftToSpawn = LevelManager.main.EnemiesPerWave();
    }

    private void EndWave()
    {
        waveStarted = false;  // Marcar que la ola ha terminado
        isSpawning = false;
        LevelManager.main.EndWave();  // Llamar a la función del LevelManager.
    }
}