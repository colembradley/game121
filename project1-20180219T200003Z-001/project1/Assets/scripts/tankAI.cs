using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankAI : MonoBehaviour {

	public float speed;
    private CharacterController controller;
    public float gravity;
    public Transform target;

    private bool seek = true;
    public bool evade = false;
    private bool readyToEvade = true;
    public float evadeSpeed;

    public int health = 100;
    private int originalHealth;
    public int projectileDamage = 5;
    private float originalSpeed;

    public bool dead = false;
    public float explosionIntensity = 50f;
    public GameObject explosion;

    public float obstacleAvoidanceDistance;
    public LayerMask obstacleAvoidanceLayers;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        target = GameObject.Find("Player").transform;
        originalSpeed = speed;
        originalHealth = health;
    }

    void Update () {

        if (seek)
        {
            //Pick move direction, avoiding obstacles
            Vector3 moveDir = Vector3.zero;
            moveDir = transform.forward;


            Vector3 raycastStart = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
            Debug.DrawRay(raycastStart, transform.forward * obstacleAvoidanceDistance, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(raycastStart, transform.forward, out hit, obstacleAvoidanceDistance, obstacleAvoidanceLayers.value))
            {
                if (hit.transform.gameObject.layer == 11)
                    moveDir = (transform.forward + transform.right * 2) / 3;
            }

            //Move
            controller.Move(moveDir.normalized * speed * Time.deltaTime/*, Space.World*/);

            //Orient
            Vector3 currentRotation = transform.eulerAngles;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        if(evade)
        {
            if (readyToEvade)
            {
                seek = false;
                readyToEvade = false;
                transform.GetChild(0).GetComponent<Animation>().Play("tankEvade");
            }
            else
            {
                if (transform.GetChild(0).GetComponent<Animation>().isPlaying)
                {
                    Vector3 evadeMove = -transform.forward * evadeSpeed * Time.deltaTime;
                    controller.Move(evadeMove);
                }
                else
                {
                    evade = false;
                    readyToEvade = true;
                    seek = true;
                    transform.GetChild(0).GetComponent<Animation>().Play("tankRun");
                }
            }
        }

        //Apply gravity
        Vector3 gravityMagnitude = Vector3.zero;
        gravityMagnitude.y -= gravity * Time.deltaTime;
        controller.Move(gravityMagnitude);

        //Slow down as health gets lower
        speed = originalSpeed * ((float)health / (float)originalHealth);

        //Die
        if(health <= 0 && !dead)
        {
            dead = true;
            seek = false;
            evade = false;
            transform.GetChild(0).GetComponent<Animation>().Stop();
            Destroy(gameObject,10);
            GameObject clone = Instantiate(explosion, transform.position, Quaternion.identity);
            clone.transform.rotation = Quaternion.LookRotation(transform.up, transform.right);
            GetComponent<CharacterController>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<Collider>().enabled = true;
                child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f,2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * explosionIntensity);
                child.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * explosionIntensity/6);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "projectile" && !evade && !dead)
        {
            evade = true;
        }

        //because yess
        if (other.gameObject.tag == "platform" && !evade && !dead)
        {
            health = 0;
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
