using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : StateMachineBehaviour
{
     private creatureMovement spawner;
     private GameManager gameManager;
     private AudioSource soundSource;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soundSource = animator.GetComponent<Transform>().parent.GetComponent<Transform>().gameObject.GetComponent<AudioSource>();
        spawner = animator.GetComponent<Transform>().parent.GetComponent<Transform>().gameObject.GetComponent<creatureMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Debug.Log("Eating");
        if(spawner.getIsCorrectFood())
        {
            gameManager.setFlag(1);
            animator.SetBool("isCorrectFood",true);
        }
        else
        {
            gameManager.setFlag(0);
            animator.SetBool("isCorrectFood",false);
        }
        spawner.setIsInArea(false);
        soundSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
