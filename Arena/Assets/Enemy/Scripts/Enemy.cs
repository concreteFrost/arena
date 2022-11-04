using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;

    public string e_name;
    public float e_health;
    public float e_speed;

    public GameObject e_weapon;
    public VariablesSO pl_pos;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        e_name = enemySO.name;
        e_health = enemySO.e_health;
        e_speed = enemySO.e_speed;
        e_weapon = enemySO.e_weapon;   
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(anim.GetBool("isInAttackRange"))
        transform.LookAt(pl_pos.pos);

    }

}
