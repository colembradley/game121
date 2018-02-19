using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotate : MonoBehaviour {

    public float rotationSpeed = 160f;

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
	}
}
