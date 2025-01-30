using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConrolerHp : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    public Slider slider;

    private void OnGUI() {
        currencyUI.text = LevelManager.main.hp.ToString();
    }

    public void SetHealt() {
        slider.value = LevelManager.main.hp;
    }
}
