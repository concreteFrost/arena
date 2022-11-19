using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStrafeBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Enemy stats;
    float timeBeforeChangeDirection;
    Vector3[] strafeDirections;

    float waitTillRunToCover;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        stats = animator.GetComponent<Enemy>();
        animator.SetBool("isAiming", true);
        strafeDirections = new[] { Vector3.up, Vector3.down };

        StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length - 1)]);

        timeBeforeChangeDirection = Random.Range(2, 5);
        waitTillRunToCover = Random.Range(5, 7);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float dist = Vector3.Distance(agent.transform.position, stats.pl_pos.pos);

        //Used to Avoid Strafe and do player chasing
        if (dist > 30)
        {
            animator.SetBool("isInAttackRange", false);
          
        }

        if (waitTillRunToCover >= 0)
        {
            waitTillRunToCover -= Time.deltaTime;
        }

        else
        {
            animator.SetBool("isCovering", true);
            animator.SetBool("isInAttackRange", false);
        }

        StrafeCalc();
      

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

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


    void StrafeCalc()
    {
        if (timeBeforeChangeDirection <= 0)
        {
            StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length)]);

            timeBeforeChangeDirection = Random.Range(3, 5);
        }
        else
        {
            timeBeforeChangeDirection -= Time.deltaTime;
        }

        if (agent.remainingDistance < 1)
        {
            StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length)]);
        }
    }
    void StrafeDirection(Vector3 dir)
    {
        agent.speed = 3;

        var dist = agent.transform.position - stats.pl_pos.pos;
      
        Vector3 enemyDirection = Vector3.Cross(dist, dir);
        agent.SetDestination(agent.transform.position - enemyDirection);
    }
}
