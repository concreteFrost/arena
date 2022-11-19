using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;

    public string name;
    public float health;
    public float speed;

    public GameObject e_weapon;
    public VariablesSO pl_pos;

    Animator anim;
    NavMeshAgent agent;

    public Transform weaponHolder;

    Rigidbody[] rBodies;

    public bool isDead;

    // Start is called before the first frame update

    private void Awake()
    {
        name = enemySO.name;
        health = enemySO.e_health;
        speed = enemySO.e_speed;
        e_weapon = Instantiate(enemySO.e_weapon, weaponHolder.position, weaponHolder.rotation);
        e_weapon.transform.parent = weaponHolder;

     
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        rBodies = GetComponentsInChildren<Rigidbody>();

        foreach(var r in rBodies)
        {
            r.isKinematic = true;
        }

    }

    private void Update()
    {
        LookAtTarget();

        if(health <= 0)
        {
            health = 0;
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

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
    }

}
