using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rescue : MonoBehaviour
{

    public VariablesSO pl_Pos;
    public NavMeshAgent agent;
    public Animator anim;
    Rigidbody[] rBodies;
    public int health = 100;
    public bool isLookingAtPlayer;
    public Transform targetToLookAt;
    public bool isDead= false;

    // Start is called before the first frame update
    void Start()
    {
  
        agent = GetComponent<NavMeshAgent>();   
        anim = GetComponent<Animator>();

        rBodies = GetComponentsInChildren<Rigidbody>();

        foreach (var r in rBodies)
        {
            r.isKinematic = true;
        }
    }

    private void Update()
    {
        isLookingAtPlayer = anim.GetBool("isFollowing");

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isLookingAtPlayer)
        {
            anim.SetLookAtWeight(0.5f);
            anim.SetLookAtPosition(targetToLookAt.position + Vector3.up * 1.5f);
        }
        else
        {
            anim.SetLookAtWeight(0);
        }
           

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

 

   
}
