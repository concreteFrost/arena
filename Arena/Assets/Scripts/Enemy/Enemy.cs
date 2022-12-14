using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;

    #region //Field of view
    public FieldOfView fov;
    public bool canSeePlayer;

    float maxFOVAngle = 100;
    public float lookRadius = 100;
    #endregion

    public string name;
    public float health;
    public float speed;

    public GameObject e_weapon;
    public VariablesSO pl_pos;

    Animator anim;
    public NavMeshAgent agent;

    public Transform weaponHolder;

    Rigidbody[] rBodies;

    public bool isDead;

    public GameEventListener playerDead;
    public GameEventListener playerShooting;


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

        foreach (var r in rBodies)
        {
            r.isKinematic = true;
        }

    }

    private void Update()
    {
        canSeePlayer = fov.CanSeePlayer(transform, maxFOVAngle, lookRadius, pl_pos, "Player");
        if (canSeePlayer)
        {
            Debug.Log("i seeeeeee");
        }
        
    }

    public void HeardTheShot()
    {
        var dist = Vector3.Distance(transform.position, pl_pos.pos);
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Patrol") && dist < 100)
            anim.SetBool("isInAttackRange", true);
    }

    public void Died()
    {
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
        if (health <= 0)
        {
            health = 0;
            Died();
        }

    }


    public void PlayerDead()
    {
        anim.SetBool("isInAttackRange", false);
        anim.SetBool("isAiming", false);
        agent.Stop();

    }

}
