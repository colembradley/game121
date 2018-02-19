using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public GameObject explosion;

    public void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject clone = Instantiate(explosion, transform.position, Quaternion.identity);
        clone.transform.rotation = Quaternion.LookRotation(transform.up, transform.right);
        Destroy(clone, 5);
        Destroy(gameObject);
    }
}
