using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseInputDebug : MonoBehaviour {

    public Transform up, down, left, right;

    public float motionScale = 10f;

    void Update () {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        up.localScale = new Vector3(1,0.1f,1);
        down.localScale = new Vector3(1, 0.1f, 1);
        left.localScale = new Vector3(1, 0.1f, 1);
        right.localScale = new Vector3(1, 0.1f, 1);

        if (mouseX > 0)
        {
            right.localScale = new Vector3(1,mouseX * motionScale,1);
        }
        if(mouseX < 0)
        {
            left.localScale = new Vector3(1, mouseX * motionScale, 1);
        }
        if (mouseY > 0)
        {
            up.localScale = new Vector3(1, mouseY * motionScale, 1);
        }
        if (mouseY < 0)
        {
            down.localScale = new Vector3(1, mouseY * motionScale, 1);
        }

    }
}
