using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TypeOfDamage
{
    general,
    head
}
public class Damagable : MonoBehaviour, IDamagable
{

    public TypeOfDamage damageType;
    public UnityEvent<int> healthEvent;

    public void TakeDamage(int damageAmount)
    {
        if (damageType == TypeOfDamage.head)
        {
            damageAmount = (int)(damageAmount * 5f) + damageAmount;

        }
        healthEvent.Invoke(damageAmount);
    }
}
