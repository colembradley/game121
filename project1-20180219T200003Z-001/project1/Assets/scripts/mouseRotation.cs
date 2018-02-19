using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseRotation : MonoBehaviour {

    public float speed = 4f;
    public bool xOrY;
    public float min;
    public float max;

    private float rotationY = 0f; 

    void Update()
    {
        if (!xOrY)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime, Space.Self);
        }
        else
        {
            rotationY = (-Input.GetAxis("Mouse Y")) * speed * Time.deltaTime;

            float x = transform.eulerAngles.x;

            if (x > 180)
            {
                x -= 360;
            }
            else if(x < -180)
            {
                x += 360;
            }
            
            rotationY = Mathf.Clamp(rotationY + x, min, max);

            transform.eulerAngles = new Vector3(rotationY , transform.eulerAngles.y, transform.eulerAngles.z);
            
        }
    }
}
