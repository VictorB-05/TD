using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public Image vida;
    public int currency;
    public int hp;
    [SerializeField] public Animator animator;

    [Header("Wave Attributes")]
    public int baseEnemies = 8;
    public float timeBetweenWaves = 2f;
    public float difficultyScalingFactor = 0.75f;
    private int currentWave = 0;

    [Header("Wave Events")]
    public UnityEvent onWaveStart = new UnityEvent();

    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 100;
        hp = 10;
        currentWave = 0;
        StartCoroutine(HandleWave());
    }

    public Transform getPath(int index) {
        return path[index];
    }

    public void increaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("Falta dinero para comprar");
            return false;
        }
    }

    public void DamageBase(int damage) {
        hp -= damage;
        vida.GetComponent<ConrolerHp>().SetHealt();
        Lose();
    }

    public void Lose() {
        if (hp <= 0) {
            animator.SetBool("GameOver", true);
        }
    }

    public void StartNextWave() {
        StartCoroutine(HandleWave());
    }

    private IEnumerator HandleWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        onWaveStart.Invoke(); // Llama a la ola actual (currentWave aún no incrementado)
        currentWave++; // Incrementa después de iniciar la ola
    }

    public int EnemiesPerWave() {
        if (currentWave == 0)
        {
            return baseEnemies;
        }
        else
        {
            return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        }
    }



    public int GetWave() {
        return currentWave;
    }
}
