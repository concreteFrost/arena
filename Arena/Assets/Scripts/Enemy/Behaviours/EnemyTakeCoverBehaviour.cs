using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTakeCoverBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;

    public float sphereRadius;
    public float maxDistance;
    float recoverWaitingTime;
    Enemy enemy;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = animator.GetComponent<NavMeshAgent>();
        recoverWaitingTime = Random.Range(2, 10);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindClosestCover(animator);
        VisibleInCover(animator);
        CalculateRecoveringTime(animator);


    }

    public void CalculateRecoveringTime(Animator animator)
    {

        if (recoverWaitingTime > 0)
        {
            recoverWaitingTime -= Time.deltaTime;
        }

        if (recoverWaitingTime <= 0)
        {
            SwitchToAttack(animator);
        }
    }

    public void VisibleInCover(Animator animator)
    {

        if (agent.isStopped && enemy.canSeePlayer==true)
        {
            Debug.Log("I SEE YOU");
            SwitchToAttack(animator);
        }
    }

    void SwitchToAttack(Animator animator)
    {

        animator.SetBool("isCovering", false);
        animator.SetBool("isInAttackRange", true);

    }


    public void FindClosestCover(Animator anim)
    {
        GameObject[] covers;
        covers = GameObject.FindGameObjectsWithTag("Cover");
        float distance = Mathf.Infinity;

        foreach (GameObject cover in covers)
        {
            Vector3 diff = cover.transform.position - anim.transform.position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance && cover.GetComponent<CoverPoint>().canSeePlayer == false)
            {
               
                distance = curDistance;
                agent.SetDestination(cover.transform.position);

                float distToCover = agent.remainingDistance;
                if (distToCover != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1)
                {
                    agent.Stop();
                }
            }
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //closest = null;
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