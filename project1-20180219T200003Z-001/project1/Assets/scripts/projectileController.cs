using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {

    public GameObject projectile;
    public float speed;
    private Vector3 direction;

	void Update () {

        direction = transform.forward;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject clone = Instantiate(projectile, transform.position, transform.parent.rotation);
            clone.GetComponent<Rigidbody>().AddForce(direction*speed);
        }
	}
}
