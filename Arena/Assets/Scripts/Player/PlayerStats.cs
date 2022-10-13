using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    male,
    female
}
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

    public void ResetParams()
    {
        health = defHealth;
        speed = defSpeed;
        stamina = defStamina;
    }

}
