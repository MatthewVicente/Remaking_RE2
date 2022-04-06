using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;

    public Rigidbody playerRigidbody;

    [Header("Camera Transform")]
    public Transform playerCamera;

    [Header("Movement Speed")]
    public float rotationSpeed = 50f;
    public float quickTurnSpeed = 8;

    [Header("Rotation Variables")]
    Quaternion targetRotation; // The place we want to rotate
    Quaternion playerRotation; // The place we are now, constantly changing

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllLocomotion()
    {
        HandleRotation();
        //HandleFalling();
    }

    public void HandleRotation()
    {
        targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
        playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (inputManager.verticalMovementInput != 0 || inputManager.horizontalMovementInput != 0)
        {
            transform.rotation = playerRotation;
        }

        if (playerManager.isPerformingQuickTurn)
        {
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, quickTurnSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }
}
