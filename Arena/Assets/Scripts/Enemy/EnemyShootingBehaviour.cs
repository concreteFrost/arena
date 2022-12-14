using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBehaviour : StateMachineBehaviour
{
    EnemyShoot enemyShoot;
    Enemy enemy;
    public List<GameObject> objectsToShoot = new List<GameObject>();
    PlayerStats player;
    Rescue[] rescues;
    float timeToSwitchTarget;
    GameObject currentTarget;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyShoot = animator.GetComponent<EnemyShoot>();
        enemy = animator.GetComponent<Enemy>();
        player = FindObjectOfType<PlayerStats>();

        if (!objectsToShoot.Contains(player.gameObject))
            objectsToShoot.Add(player.gameObject);

        rescues = FindObjectsOfType<Rescue>();

       
        timeToSwitchTarget = Random.Range(3, 10);
        currentTarget = objectsToShoot[Random.Range(0, objectsToShoot.Count)];
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindOtherTargets();
        

        if (timeToSwitchTarget > 0)
        {
            timeToSwitchTarget -= Time.deltaTime;
            
            enemyShoot.Shoot(currentTarget.transform.position);
        
    
        }
        
        if(timeToSwitchTarget <= 0)
        {
            currentTarget = objectsToShoot[Random.Range(0, objectsToShoot.Count)];
            timeToSwitchTarget = Random.Range(3, 10);
        }
        

       
    }

    void RotateTowardsTarget(Animator animator, GameObject currentTarget)
    {
        var lookPos = currentTarget.transform.position - animator.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotation, Time.deltaTime * 2f);
    }

    void FindOtherTargets()
    {
        foreach (Rescue rescue in rescues)
        {
            var dist = Vector3.Distance(enemy.pl_pos.pos, rescue.transform.position);

            if (dist < 25 && !rescue.isDead)
            {
                if (!objectsToShoot.Contains(rescue.gameObject))
                    objectsToShoot.Add(rescue.gameObject);
            }
            else
            {
                objectsToShoot.Remove(rescue.gameObject);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(new Vector3(currentTarget.transform.position.x, animator.transform.position.y, currentTarget.transform.position.z));
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}



}
