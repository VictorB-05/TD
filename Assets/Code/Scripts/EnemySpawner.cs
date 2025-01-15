using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpwan;
    private int enemiesAlive;
    public int enemiesDead;
    private int enemiesLeftToSpawn;
    private bool isSpawing = false;

    private void Start()
    {
        StartWave();
    }

    private void Update(){
        if (!isSpawing) return;

        timeSinceLastSpwan += Time.deltaTime;

        if(timeSinceLastSpwan >= (1f / enemiesPerSecond)){
            Debug.Log("Spwan Enemy");
            timeSinceLastSpwan = 0f;
        }
    }

    private void StartWave(){
        isSpawing=true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
