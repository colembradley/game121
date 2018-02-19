using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {

    public Vector3 pivot;
    public float speed;

	void Update () {
        transform.rotation *= Quaternion.AngleAxis(speed*Time.deltaTime, pivot);
	}
}
