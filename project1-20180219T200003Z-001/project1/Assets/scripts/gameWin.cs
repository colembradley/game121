using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameWin : MonoBehaviour {

    public GameObject winCamera;
    public thirdPersonController playerScript;
    public firstPersonControls playerScript2;
    public projectileController projectileController;
    public clickToTeleport teleportScript;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(playerScript);
            Destroy(playerScript2);
            Destroy(projectileController);
            Destroy(teleportScript);
            winCamera.SetActive(true);
        }
    }
}
