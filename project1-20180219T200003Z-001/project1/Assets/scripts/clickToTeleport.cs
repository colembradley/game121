using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToTeleport : MonoBehaviour {

    public bool canTeleport = true;
    public Transform player;
    public float playerHeight;

    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 desiredPosition = hit.point;
                desiredPosition.y += playerHeight;
                player.transform.position = desiredPosition;
            }
        }
    }

}
