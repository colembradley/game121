using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyAI : MonoBehaviour {

    public float speed;
    private CharacterController controller;
    public float gravity;
    public Transform target;

    private bool seek = false;
    private bool useRaycast = true;
    public float raycastLength;
    public LayerMask playerDetectionLayers;

    public int health = 100;
    public int projectileDamage = 5;

    public bool dead = false;
    public float explosionIntensity = 50f;
    public GameObject explosion;

    public float obstacleAvoidanceDistance;
    public LayerMask obstacleAvoidanceLayers;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (useRaycast)
        {
            Vector3 raycastDir = target.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, raycastDir, out hit, raycastLength, playerDetectionLayers.value))
            {
                seek = true;
            }
            else
            {
                seek = false;
            }
        }

        if (seek)
        {
            //Pick move direction, avoiding obstacles
            Vector3 moveDir = Vector3.zero;
            moveDir = transform.forward;


            Vector3 raycastStart = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
            Debug.DrawRay(raycastStart, transform.forward*obstacleAvoidanceDistance, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(raycastStart, transform.forward, out hit, obstacleAvoidanceDistance, obstacleAvoidanceLayers.value))
            {
                if(hit.transform.gameObject.layer == 11)
                    moveDir = (transform.forward + transform.right*2)/3;
            }
            
            //Move
            controller.Move(moveDir.normalized * speed * Time.deltaTime/*, Space.World*/);

            //Orient
            Vector3 currentRotation = transform.eulerAngles;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        //Apply gravity
        Vector3 gravityMagnitude = Vector3.zero;
        gravityMagnitude.y -= gravity * Time.deltaTime;
        controller.Move(gravityMagnitude);

        //Die
        if (health <= 0 && !dead)
        {
            dead = true;
            useRaycast = false;
            seek = false;
            Destroy(gameObject, 10);
            GameObject clone = Instantiate(explosion, transform.position, Quaternion.identity);
            transform.GetChild(0).GetComponent<spin>().enabled = false;
            clone.transform.rotation = Quaternion.LookRotation(transform.up, transform.right);
            GetComponent<CharacterController>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<Collider>().enabled = true;
                child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * explosionIntensity);
                child.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * explosionIntensity / 6);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            health -= projectileDamage;
        }
    }
}
