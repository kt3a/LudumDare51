using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : StateMachineBehaviour
{
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    
  //}

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (stateInfo.normalizedTime > 0.4f && stateInfo.normalizedTime < 0.7f)
    {
      //yes this is the best way to find the reference stop asking
      animator.transform.parent.parent.gameObject.GetComponentInChildren<MeleeHitbox>().EnableHit();
    }
    else
    {
      animator.transform.parent.parent.gameObject.GetComponentInChildren<MeleeHitbox>().DisableHit();
    }
  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.transform.parent.parent.gameObject.GetComponentInChildren<MeleeHitbox>().DisableHit();
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
