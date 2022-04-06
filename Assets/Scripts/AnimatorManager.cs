using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    PlayerManager playerManager;
    PlayerLocomotionManager playerLocomotionManager;

    float snappedHorizontal;
    float snappedVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    public void HandleAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        if(horizontalMovement > 0)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }

        if(verticalMovement > 0)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }

        if(isRunning)
        {
            snappedVertical = 2;
        }

        animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
    }

    public void PlayAnimationWithoutRootMotion(string targetAnimaiton, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.SetBool("disableRootMotion", true);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimaiton, 0.2f);
    }

    private void OnAnimatorMove()
    {
        if(playerManager.disableRootMotion)
        {
            return;
        }

        Vector3 animatorDeltaPosition = animator.deltaPosition;
        animatorDeltaPosition.y = 0;

        Vector3 velocity = animatorDeltaPosition / Time.deltaTime;
        playerLocomotionManager.playerRigidbody.drag = 0;
        playerLocomotionManager.playerRigidbody.velocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }
}
