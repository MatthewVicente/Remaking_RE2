using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    [Header("Is Preforming Disable Root Motion")]
    public string disableRootMotion = "disableRootMotion";
    public bool disableRootMotionStatus = false;

    [Header("Is Preforming action Bool")]
    public string isPerformingAction = "isPerformingAction";
    public bool isPerformingActionStatus = false;

    [Header("Is Preforming Quick Turn")]
    public string isPerformingQuickTurn = "isPerformingQuickTurn";
    public bool isPerformingQuickTurnStatus = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isPerformingAction, isPerformingActionStatus);
        animator.SetBool(isPerformingQuickTurn, isPerformingQuickTurnStatus);
        animator.SetBool(disableRootMotion, disableRootMotionStatus);
    }
}
