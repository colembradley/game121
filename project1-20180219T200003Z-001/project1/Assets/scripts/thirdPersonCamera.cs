using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour {

    public Transform player;
    public Transform mainCamera;

    [Header("Zoom")]
    public bool canZoom = true;
    public float zoomMin;
    public float zoomMax;
    public float zoomSpeed = 1f;
    public float firstPersonPoint;

    [Header("First Person Things")]
    public Camera firstPersonCamera;
    public mouseRotation firstPersonCameraRotation;
    public mouseRotation firstPersonCameraRotation2;
    public firstPersonControls firstPersonControls;

    [Header("Third Person Things")]
    public Camera thirdPersonCameraComponent;
    public mouseRotation thirdPersonCameraRotation;
    public mouseRotation thirdPersonCameraRotation2;
    public thirdPersonController thirdPersonController;
    public clickToTeleport teleportScript;


    void Start()
    {
        mainCamera = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        if (canZoom)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                float desiredDistance = mainCamera.transform.localPosition.z + Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
                if (desiredDistance < zoomMax)
                {
                    Vector3 newPosition = mainCamera.transform.localPosition;
                    newPosition.z = desiredDistance;
                    mainCamera.transform.localPosition = newPosition;
                }
                if (desiredDistance > firstPersonPoint)
                {
                    thirdPersonCameraComponent.enabled = false;
                    thirdPersonCameraRotation.enabled = false;
                    thirdPersonCameraRotation2.enabled = false;
                    thirdPersonController.enabled = false;
                    teleportScript.enabled = false;
                    firstPersonCamera.enabled = true;
                    firstPersonCameraRotation.enabled = true;
                    firstPersonCameraRotation2.enabled = true;
                    firstPersonControls.enabled = true;
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                float desiredDistance = mainCamera.transform.localPosition.z + Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
                if (desiredDistance > zoomMin) 
                {
                    Vector3 newPosition = mainCamera.transform.localPosition;
                    newPosition.z = desiredDistance;
                    mainCamera.transform.localPosition = newPosition;
                }
                if (desiredDistance < firstPersonPoint)
                {
                    thirdPersonCameraComponent.enabled = true;
                    thirdPersonCameraRotation.enabled = true;
                    thirdPersonCameraRotation2.enabled = true;
                    thirdPersonController.enabled = true;
                    teleportScript.enabled = true;
                    firstPersonCamera.enabled = false;
                    firstPersonCameraRotation.enabled = false;
                    firstPersonCameraRotation2.enabled = false;
                    firstPersonControls.enabled = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        transform.position = player.position;
    }
}
