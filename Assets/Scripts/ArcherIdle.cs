using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdle : StateMachineBehaviour
{
    Transform knight;
    private int runRange = 150;
    private int aimDistance = 35;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.transform.GetComponent<Archer>().getHP() <= 0)
            animator.SetBool("isAlive", false);


        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Knight");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - animator.transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest)
        {
            knight = closest.transform;
            animator.transform.LookAt(knight);

            distance = Vector3.Distance(knight.position, animator.transform.position);
            if (distance < aimDistance)
                animator.SetBool("isAiming", true);

            else if (distance > runRange)
                animator.SetBool("isWalking", true);

            else if(distance < runRange && distance > aimDistance)
                animator.SetBool("isRunning", true);
        }
        





    }

    //  OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
