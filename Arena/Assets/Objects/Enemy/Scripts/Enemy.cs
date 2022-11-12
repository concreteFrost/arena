using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;

    public string e_name;
    public float e_health;
    public float e_speed;

    public GameObject e_weapon;
    public VariablesSO pl_pos;

    Animator anim;
    NavMeshAgent agent;

    public Transform weaponHolder;

    CapsuleCollider[] damageable;
    Rigidbody[] rBodies;

    public bool isDead;

    // Start is called before the first frame update

    private void Awake()
    {
        e_name = enemySO.name;
        e_health = enemySO.e_health;
        e_speed = enemySO.e_speed;
        e_weapon = Instantiate(enemySO.e_weapon, weaponHolder.position, weaponHolder.rotation);
        e_weapon.transform.parent = weaponHolder;

     
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        damageable = GetComponentsInChildren<CapsuleCollider>();
        rBodies = GetComponentsInChildren<Rigidbody>();

        foreach(var r in rBodies)
        {
            r.isKinematic = true;
        }

        foreach(var c in damageable)
        {
            c.GetComponent<EnemyDamagable>().health = this;
        }

    }

    private void Update()
    {
        LookAtTarget();

        if(e_health <= 0)
        {
            e_health = 0;
            Died();
        }

    }

    void LookAtTarget()
    {

        if (anim.GetBool("isInAttackRange") && !isDead)
            transform.LookAt(pl_pos.pos);
    }

    public void Died()
    {
        anim.SetBool("isDead", true);
        
        foreach (var r in rBodies)
        {
            r.isKinematic = false;
        }

        anim.enabled = false;
        agent.enabled = false;
        isDead = true;
        
    }

}
