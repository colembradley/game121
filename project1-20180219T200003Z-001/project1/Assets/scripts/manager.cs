using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour {

    public bool gameWon = false;
	
	void Update () {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            gameWon = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
