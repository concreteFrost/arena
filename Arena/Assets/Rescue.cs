using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rescue : MonoBehaviour
{

    public VariablesSO pl_Pos;
    public NavMeshAgent agent;
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
  
        agent = GetComponent<NavMeshAgent>();   
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wait()
    {
        anim.SetBool("isFollowing",false);

    }

    public void Follow()
    {
        anim.SetBool("isFollowing", true);
        anim.SetBool("isCovering", false);

    }

    public void TakeCover()
    {
        anim.SetBool("isCovering", true);
        anim.SetBool("isFollowing", false);
    }


   
}
