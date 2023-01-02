using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rescue : MonoBehaviour
{

    public VariablesSO pl_Pos;
    public GameEventSO rescueSaved;
    public GameEventSO rescueDead;
    public NavMeshAgent agent;
    public Animator anim;
    Rigidbody[] rBodies;
    public int health = 100;
    public bool isLookingAtPlayer;
    public Transform targetToLookAt;
    public bool isDead= false;
    Transform rescuePoint;

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

        rescuePoint = GameObject.FindGameObjectWithTag("Rescue Point").transform;
    }

    private void Update()
    {
        isLookingAtPlayer = anim.GetBool("isFollowing");
        RescueSaved();

        if (health <= 0)
        {
            RescueDead();
        }

    }

    void RescueSaved()
    {
        var dist = Vector3.Distance(transform.position, rescuePoint.position);

        if(dist < 5)
        {
            rescueSaved.Raise();
            gameObject.SetActive(false);
            
        }
    }

    void RescueDead()
    {
        rescueDead.Raise();
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
