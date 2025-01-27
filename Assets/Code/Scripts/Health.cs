using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 20;

    private bool isDestroyed=false;

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;    

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.increaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
