using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;

public class EnemyChaseBehaviour : StateMachineBehaviour
{

    NavMeshAgent agent;
    Enemy stats;
    Vector3 lastSeenPosition;
    float currentHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        stats = animator.GetComponent<Enemy>();
        animator.SetBool("isAiming", false);
        lastSeenPosition = Vector3.zero;
        currentHealth = stats.health;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = stats.speed;

        var dist = Vector3.Distance(animator.transform.position, stats.pl_pos.pos);


       

       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
