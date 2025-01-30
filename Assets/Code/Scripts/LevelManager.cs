using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public Image vida;
    public int currency;
    public int hp;


    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 100;
        hp = 10;
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

    private void Lose() {
        if (hp < 0) {
            Debug.Log("Has perdido");
        }
    }
}
