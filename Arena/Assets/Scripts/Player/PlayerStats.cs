using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public string name;
    
    public float speed;
    public float defSpeed;
    
    public float health;
    public float defHealth;
   
    public float stamina;
    public float defStamina;
    
    public float minPrice;
    public float price;
 
    public Gender gender;

    public PlayerStatsSO pl_stats;
    public VariablesSO pl_position;

    private void Start()
    {
        name = pl_stats.name;

        speed = pl_stats.speed;
        defSpeed = pl_stats.defSpeed;

        health = pl_stats.health;
        defHealth = pl_stats.defHealth;

        minPrice = pl_stats.minPrice;
        price = pl_stats.price;

        gender = pl_stats.gender;
    }

    public void ResetParams()
    {
        health = defHealth;
        speed = defSpeed;
        stamina = defStamina;
    }

    private void Update()
    {
        pl_position.pos = transform.position;
    }

}
