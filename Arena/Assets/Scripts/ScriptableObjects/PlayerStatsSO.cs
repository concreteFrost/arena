using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    male,
    female
}
[CreateAssetMenu(menuName ="Player/Player Stats")]
public class PlayerStatsSO : ScriptableObject
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

    public GameObject[] availableCharacters;
    public GameObject character;

    public GameObject additionalItem;
    public GameObject starterWeapon;

    public void ResetParams()
    {
        name = "";
        health = defHealth;
        speed = defSpeed;
        stamina = defStamina;
        price = minPrice;
        gender = Gender.male;
        character = availableCharacters[0];
    }
}
