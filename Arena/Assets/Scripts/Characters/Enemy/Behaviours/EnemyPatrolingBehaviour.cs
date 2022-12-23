using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class EnemyPatrolingBehaviour : StateMachineBehaviour
{
    Enemy enemy;
    float waitBeforeMove;
    float currentHealth;
    int pointIndex;
    public List<Transform> patrolPoints = new List<Transform> ();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        waitBeforeMove = Random.Range(3, 7f);
        animator.SetBool("isAiming", false);
        currentHealth = enemy.health;
       
        patrolPoints = FindObjectOfType<PatrolPoints>().allZones.Find(x => x.zoneIndex == pointIndex).zones;
        pointIndex = Random.Range(0, patrolPoints.Count-1);

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
            pointIndex = Random.Range(0, patrolPoints.Count - 1);
            Patrol(animator,enemy.agent);
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

    public void Patrol(Animator anim,NavMeshAgent agent)
    {
        agent.SetDestination(patrolPoints[pointIndex].position);
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
