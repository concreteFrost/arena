using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image weaponIcon;
    public Text weaponText;
    public Text bulletAmountText;

    public Slider healthbar;
    float currHealthValue;
    PlayerStats plStats;

    public GameObject rescueCommands;
    // Start is called before the first frame update
    void Start()
    {
        plStats = GetComponent<PlayerStats>();
        healthbar.maxValue = plStats.health;
        healthbar.value = plStats.health;
        currHealthValue = plStats.health;
        rescueCommands.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        HealthSliderDisplay();
    }

    void HealthSliderDisplay()
    {
        currHealthValue = plStats.health;

        if (healthbar.value > currHealthValue)
        {
            healthbar.value -= Time.deltaTime * 20;
        }

        if (healthbar.value < currHealthValue)
        {
            healthbar.value += Time.deltaTime * 20;
        }


    }
}
