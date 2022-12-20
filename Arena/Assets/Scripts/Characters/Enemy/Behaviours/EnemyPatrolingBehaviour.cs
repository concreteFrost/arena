using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolingBehaviour : StateMachineBehaviour
{
    Enemy enemy;
    float waitBeforeMove;
    float currentHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        waitBeforeMove = Random.Range(3, 10f);
        animator.SetBool("isAiming", false);
        currentHealth = enemy.health;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.agent.speed = 3;

        var dist = Vector3.Distance(animator.transform.position, enemy.pl_pos.pos);

        if (enemy.agent.remainingDistance <= 1f)
        {
            enemy.agent.Stop();

        }

        if (waitBeforeMove > 0)
        {
            waitBeforeMove -= Time.deltaTime;
            
        }

        if(waitBeforeMove <= 0)
        {
          
            enemy.agent.Resume();
            enemy.agent.SetDestination(RandomNavmeshLocation(Random.Range(10f, 20f), animator));
            waitBeforeMove = Random.Range(3, 10f);
        }

        if (currentHealth > enemy.health || dist < 30 && enemy.canSeePlayer)
        {

            animator.SetBool("isInAttackRange", true);
 
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.agent.speed = enemy.speed;
    }

    public Vector3 RandomNavmeshLocation(float radius, Animator animator)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += animator.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

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
