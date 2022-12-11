using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStrafeBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Enemy stats;
    public float timeBeforeChangeDirection;
    Vector3[] strafeDirections = new[] { Vector3.up, Vector3.down };

    public float waitTillRunToCover;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.Resume();

        stats = animator.GetComponent<Enemy>();
        animator.SetBool("isAiming", true);
       
        StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length)]);
        timeBeforeChangeDirection = Random.Range(2, 5);
        waitTillRunToCover = Random.Range(7, 15);
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
            StrafeCalc();
        }

        else
        {
            animator.SetBool("isCovering", true);
            animator.SetBool("isInAttackRange", false);
        }

        
      

    }

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
        agent.speed = 3.3f;

        var dist = agent.transform.position - stats.pl_pos.pos;
      
        Vector3 enemyDirection = Vector3.Cross(dist, dir);
        agent.SetDestination(agent.transform.position - enemyDirection);
    }
}
