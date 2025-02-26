using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private int hitPointsBase = 2;
    [SerializeField] private int hitPoints = 0;
    [SerializeField] private int currencyWorth = 20;

    private bool isDestroyed=false;

    private void Start() {
        hitPoints = hitPointsBase * (LevelManager.main.GetWave()); // Inicializar la vida con el valor base
        Debug.Log(hitPoints+"  "+LevelManager.main.GetWave());
    }

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;    

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.increaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public int GetHP() {
        return hitPoints;
    }

}
