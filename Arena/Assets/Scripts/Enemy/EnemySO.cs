using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    public string e_name;
    public float e_health;
    public float e_speed;

    public GameObject e_weapon;

}
