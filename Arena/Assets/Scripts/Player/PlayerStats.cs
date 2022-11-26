using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using StarterAssets;


public class PlayerStats : MonoBehaviour
{
    public string name;
    
    public float speed;
    public float defSpeed;
    
    public float health;
    public float defHealth;
   
    public float stamina;
    public float defStamina;
    
    public float minPrice;
    public float price;
 
    public Gender gender;

    public PlayerStatsSO pl_stats;
    public VariablesSO pl_position;

    CharacterController controller;
    Animator animator;
    public CapsuleCollider[] colliders;
    Rigidbody[] rbs;
    RigBuilder rig;
    ThirdPersonController thirdPersonController;

    public GameEventSO playerDeadEvent;
    

    private void Awake()
    {
        colliders = GetComponentsInChildren<CapsuleCollider>();
        rbs = GetComponentsInChildren<Rigidbody>();

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        foreach(Rigidbody r in rbs)
        {
            r.isKinematic = true;
        }

        name = pl_stats.name;

        speed = pl_stats.speed;
        defSpeed = pl_stats.defSpeed;

        health = pl_stats.health;
        defHealth = pl_stats.defHealth;

        minPrice = pl_stats.minPrice;
        price = pl_stats.price;

        gender = pl_stats.gender;
       
    }

    private void Start()
    {
       

        thirdPersonController = GetComponent<ThirdPersonController>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rig = GetComponentInChildren<RigBuilder>();
        health = 20;
    }

    public void ResetParams()
    {
        health = defHealth;
        speed = defSpeed;
        stamina = defStamina;
    }

    private void Update()
    {
        pl_position.pos = transform.position;

        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    public void Die()
    {
        controller.enabled = false;
        animator.enabled = false;

        foreach (var r in rbs)
        {
            r.isKinematic = false;
        }

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        playerDeadEvent.Raise();
        thirdPersonController.enabled = false;
        rig.enabled = false;

    }

}
