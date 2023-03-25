using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealth;


    private void Awake() {
        
        healthBar.maxValue = playerHealth.GetMaxHealth();

    }

    private void Update() {
        healthBar.value = playerHealth.GetHealth();
    }


}
