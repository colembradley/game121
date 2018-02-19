using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonController : MonoBehaviour {

    public Transform mainCamera;
    public float speed = 4f;
    public float gravity = 6f;
    public float jumpStrength = 100f;
    public bool isJumping = false;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        //float moveHorizontal = Input.GetAxisRaw("Horizontal");
        //float moveVertical = Input.GetAxisRaw("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        

        Vector3 cameraForward = new Vector3(mainCamera.forward.x, 0f, mainCamera.forward.z);
        Vector3 cameraRight = new Vector3(mainCamera.right.x, 0f, mainCamera.right.z);

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += cameraForward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += -cameraForward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += -cameraRight;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += cameraRight;
        }

        //transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);

        /*
        //Jump
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpStrength;
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
        }
        */

        //Move player
        controller.Move(moveDir.normalized * speed * Time.deltaTime/*, Space.World*/);

        //Orient player
        if (moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }




        //Apply gravity
        if (!isJumping)
        {
            Vector3 gravityMagnitude = Vector3.zero;
            gravityMagnitude.y -= gravity * Time.deltaTime;
            controller.Move(gravityMagnitude);
        }

    }
}
