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
    [SerializeField]  private int currentWave = 0;
    private float timeSinceLastSpwan;
    [SerializeField] private int enemiesAlive;
    [SerializeField] public int enemiesDead;
    private int enemiesLeftToSpawn;
    private bool isSpawing = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        LevelManager.main.onWaveStart.AddListener(StartWave);
    }

    private void Update(){
        if (!isSpawing) return;

        timeSinceLastSpwan += Time.deltaTime;

        if (timeSinceLastSpwan >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpwan = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            isSpawing = false;
            LevelManager.main.EndWave();  // Llamar a la función del LevelManager.
        }
    }

    private void EnemyDestroyed(){
        enemiesAlive--;
    }

    private void SpawnEnemy() { 
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void StartWave() {
        isSpawing = true;
        enemiesLeftToSpawn = LevelManager.main.EnemiesPerWave();  // Usar la lógica de olas del LevelManager
        
    }

}
