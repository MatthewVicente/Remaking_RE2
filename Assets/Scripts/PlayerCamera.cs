using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public InputManager inputManager;

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 targetPosition;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    [Header("Camera Speed")]
    float cameraSmoothTime = 0.1f;

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maximunPivotAngle = 15;
    float minimumPivotAngle = -15;

    public void HandleAllCameraMovement()
    {
        //follow the player
        FollowPlayer();
        //rotate the camera
        RotateCamera();
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAmountVertical = lookAmountVertical + (inputManager.horizontalCameraInput);
        lookAmountHorizontal = lookAmountHorizontal - (inputManager.verticalCameraInput);
        lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minimumPivotAngle, maximunPivotAngle);

        cameraRotation = Vector3.zero;
        cameraRotation.y = lookAmountVertical;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
        transform.rotation = targetRotation;

        //IF WE ARE PERFORMING A QUICK TURN, WE NEED TO SNAP OUR CAMERA 180

        if(inputManager.quickTurnInput)
        {
            inputManager.quickTurnInput = false;
            lookAmountVertical = lookAmountVertical + 180;
            cameraRotation.y = cameraRotation.y + 180;
            transform.rotation = targetRotation;
            //IN FUTURE SMOOTH TRANSITION
        }

        cameraRotation = Vector3.zero;
        cameraRotation.x = lookAmountHorizontal;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
        cameraPivot.localRotation = targetRotation;
    }
}
