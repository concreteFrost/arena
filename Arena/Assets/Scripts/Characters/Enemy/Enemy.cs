using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, ISpawnable
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
    public int patrolZoneIndex;

    Rigidbody[] rBodies;

    public bool isDead;

    public GameEventListener playerDead;
    public GameEventListener playerShooting;

    public int amountToSpawn;
    public GameEventSO respawn;
    Vector3 respawnPosition;


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
            r.isKinematic = true;

        respawnPosition = transform.position;

    }

    private void Update()
    {
        canSeePlayer = fov.CanSeePlayer(transform, maxFOVAngle, lookRadius, pl_pos, "Player");
    }


    public void HeardTheShot()
    {
        var dist = Vector3.Distance(transform.position, pl_pos.pos);
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Patrol") && dist < enemySO.hearingDistance)
            StartCoroutine(SwitchToShootMode(2f));
    }

    public void Died()
    {
        anim.enabled = false;
        agent.enabled = false;
        foreach (var r in rBodies)
        {
            r.isKinematic = false;
        }

      
        isDead = true;
        ResetWeapon();
       
 
        StartCoroutine(DeactivateObject());
        
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

    public void LookAtTarget(GameObject target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position),0.01f * Time.deltaTime);
    }


    public void PlayerDead()
    {
        anim.SetBool("isInAttackRange", false);
        anim.SetBool("isAiming", false);
        agent.Stop();

    }

    public void SwtichToShoot()
    {
        StartCoroutine(SwitchToShootMode(0.5f));
    }

    public IEnumerator SwitchToShootMode(float s)
    {
        yield return new WaitForSeconds(s);
        anim.SetBool("isInAttackRange", true);
    }

    int ISpawnable.DeductFromSpawn(int deductFromAmount)
    {
       return amountToSpawn -= deductFromAmount;
    }

    IEnumerator DeactivateObject()
    {
        yield return new WaitForSeconds(300f);
        gameObject.SetActive(false);
        if (amountToSpawn > 0)
        {
            respawn.Raise();
            Respawn();
        }
            
    }

    void Respawn()
    {
        anim.enabled = true;
        agent.enabled = true;
        isDead = false;
        e_weapon = Instantiate(enemySO.e_weapon, weaponHolder.position, weaponHolder.rotation);
        e_weapon.GetComponent<BoxCollider>().enabled = false;
        e_weapon.transform.parent = weaponHolder;
        health = enemySO.e_health;
        transform.position = respawnPosition;
    }

    void ResetWeapon()
    {
        e_weapon.GetComponent<BoxCollider>().enabled = true;
        e_weapon.transform.SetParent(null);
        e_weapon.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        e_weapon.transform.rotation = new Quaternion(0, 0, 0, 0);
        e_weapon = null;
    }
}
