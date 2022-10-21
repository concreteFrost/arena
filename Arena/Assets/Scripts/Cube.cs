using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IDamagable
{
    public int health;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

   
   
}
