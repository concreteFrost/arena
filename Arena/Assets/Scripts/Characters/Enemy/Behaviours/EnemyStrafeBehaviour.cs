using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStrafeBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Enemy stats;
    public float timeBeforeChangeDirection;
    public float playerIsVisibleTimer;
    float defaultVisibleInTemer = 2f;
    Vector3[] strafeDirections = new[] { Vector3.up, Vector3.down };
    float randomHealthStats;
    public Vector3 lastSeenPosition;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.Resume();

        stats = animator.GetComponent<Enemy>();
       
       
        StrafeDirection(strafeDirections[Random.Range(0, strafeDirections.Length)]);
        timeBeforeChangeDirection = Random.Range(2, 5);
        playerIsVisibleTimer = defaultVisibleInTemer;
        lastSeenPosition = Vector3.zero;
        randomHealthStats = stats.health - (stats.health * Random.Range(0.2f, 0.5f));
        agent.SetDestination(stats.pl_pos.pos);
        animator.SetBool("isAiming", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.Resume();
        var dist = Vector3.Distance(animator.transform.position, stats.pl_pos.pos);

        //Do strafe run while fighting
        if (stats.canSeePlayer) {
            StrafeCalc();
            
        }

        //Take Cover
        if (stats.health < randomHealthStats)
        {
            
            animator.SetBool("isCovering", true);
            animator.SetBool("isInAttackRange", false);
        }

        //Countdown time while player is not visible
        if (!stats.canSeePlayer && playerIsVisibleTimer>0)
        {
            playerIsVisibleTimer  -= Time.deltaTime;
        }

        //Keep timer in default state while player is visible
        if (stats.canSeePlayer)
        {
            playerIsVisibleTimer = defaultVisibleInTemer;
        }

        //Set destination to last seen position
        if(playerIsVisibleTimer <= 0 && lastSeenPosition!=stats.pl_pos.pos)
        {

            playerIsVisibleTimer = defaultVisibleInTemer;
            lastSeenPosition = stats.pl_pos.pos;
            agent.SetDestination(lastSeenPosition);
            
        }

        //Switch back to patrol mode
        if(lastSeenPosition == stats.pl_pos.pos && agent.remainingDistance < 1)
        {
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

        if (agent.remainingDistance < 2)
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
