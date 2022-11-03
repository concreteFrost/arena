using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStrafeBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Enemy stats;
    public VariablesSO pl_pos;
    float timeBeforeChange;
    string[] strafeDirections;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        stats = animator.GetComponent<Enemy>();
        timeBeforeChange = Random.Range(2, 5);
        strafeDirections = new[] { "left", "right" };
        StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length - 1)]);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 4;
        float dist = Vector3.Distance(agent.transform.position, pl_pos.pos);
        if(dist > 15)
        {
            animator.SetBool("isInAttackRange", false);
        }


    

        if(timeBeforeChange <= 0)
        {
            StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length)]);
            
            timeBeforeChange = Random.Range(3, 5);
        }
        else
        {
            timeBeforeChange -= Time.deltaTime;
        }
  
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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

    void StrafeDirection(string dir)
    {
            var dist = agent.transform.position - pl_pos.pos;
           
        Vector3 enemyDirection;
        switch (dir)
        {
            case "left":
                enemyDirection = Vector3.Cross(dist, Vector3.up);
                agent.SetDestination(agent.transform.position - enemyDirection);
                Debug.Log("left");
                break;
            case "right":
                enemyDirection = Vector3.Cross(dist, Vector3.down);
                agent.SetDestination(agent.transform.position - enemyDirection);
                Debug.Log("right");
                break;
        }
    }
}
