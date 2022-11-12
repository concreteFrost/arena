using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfDamage
{
    general,
    head
}
public class EnemyDamagable : MonoBehaviour, IDamagable
{

    public Enemy health;
    public TypeOfDamage damageType;

  
    public void TakeDamage(int damageAmount)
    {
        if(damageType == TypeOfDamage.head)
        {
            var addToDamage = (damageAmount * 0.5f) + damageAmount;
            health.e_health -= addToDamage;
        }
        health.e_health -= damageAmount;


       
    }
}
