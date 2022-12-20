using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RescueTakeCoverBehaviour : StateMachineBehaviour
{

    Rescue rescue;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rescue = animator.GetComponent<Rescue>();
       
       rescue.agent.speed = 4.7f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("here");
        FindClosestCover(animator, rescue.agent);
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

    public void FindClosestCover(Animator anim, NavMeshAgent agent)
    {
        rescue.agent.Resume();
        GameObject[] covers = GameObject.FindGameObjectsWithTag("Cover");
        float distance = Mathf.Infinity;

        foreach (GameObject cover in covers)
        {
            Vector3 diff = cover.transform.position - anim.transform.position;
            float curDistance = diff.sqrMagnitude;

            //add canSeePlayer if necessary
            if (curDistance < distance)
            {
                distance = curDistance;
                agent.SetDestination(cover.transform.position);

                float distToCover = agent.remainingDistance;
                if (distToCover <1 && agent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    agent.Stop();
                }
            }
        }

    }
}
