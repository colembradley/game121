using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonControls : MonoBehaviour {

    //public float speed = 4f;

    private CharacterController controller;

    public thirdPersonController thirdPersonScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update () {

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += -transform.forward;
        }                                   
        if (Input.GetKey(KeyCode.A))        
        {
            moveDir += -transform.right;
        }                                   
        if (Input.GetKey(KeyCode.D))        
        {
            moveDir += transform.right;
        }
        //transform.position += moveDir.normalized * Time.deltaTime * speed;
        controller.Move(moveDir.normalized * Time.deltaTime * thirdPersonScript.speed);

        //Apply gravity
        Vector3 gravityMagnitude = Vector3.zero;
        gravityMagnitude.y -= thirdPersonScript.gravity * Time.deltaTime;
        controller.Move(gravityMagnitude);

    }
}
