using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RescueFollowPlayer : StateMachineBehaviour
{
    Rescue rescue;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rescue = animator.GetComponent<Rescue>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var dist = Vector3.Distance(rescue.agent.transform.position, rescue.pl_Pos.pos);

            FollowPlayer(dist);

        SpeedControl(dist, rescue.agent);
    }

    void FollowPlayer(float dist)
    {
        if (dist > 3)
        {
            rescue.agent.Resume();
            rescue.agent.SetDestination(rescue.pl_Pos.pos);
        }

        else
            rescue.agent.Stop();
    }

    void SpeedControl(float dist,  NavMeshAgent agent)
    {
        if (dist > 6)
        {
            agent.speed = 4;
        }
        else
            agent.speed =2;

        agent.speed = Mathf.Clamp(agent.speed, 2, 4.7f);
    
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
}
