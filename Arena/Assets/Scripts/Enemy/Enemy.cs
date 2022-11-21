using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;

    #region //Field of view
    FieldOfView fov;
    public bool canSeePlayer;

    float maxFOVAngle = 100;
    public float lookRadius = 300;
    #endregion

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

    public GameEventListener playerDead;


    // Start is called before the first frame update

    private void Awake()
    {
        name = enemySO.name;
        health = enemySO.e_health;
        speed = enemySO.e_speed;
        e_weapon = Instantiate(enemySO.e_weapon, weaponHolder.position, weaponHolder.rotation);
        e_weapon.transform.parent = weaponHolder;
        fov = GetComponent<FieldOfView>();
     
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
        if (anim.GetBool("isInAttackRange") && !isDead || agent.isStopped == true && !isDead)
            LookAtTarget();

        canSeePlayer = fov.CanSeePlayer(transform, maxFOVAngle, lookRadius, pl_pos, "Player");
        if (health <= 0)
        {
            health = 0;
            Died();
        }

    }

    void LookAtTarget()
    {
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

    public void PlayerDead()
    {
        anim.SetBool("isInAttackRange", false);
        anim.SetBool("isAiming", false);
        agent.Stop();

    }

}
