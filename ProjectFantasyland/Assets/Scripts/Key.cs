using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private void OnTriggerEnter(Collider collide) {
        if (collide.transform.tag == "Player") {
            PlayerMovement.keyObtained = true;
            PlayerStopwatch.stopwatch = 2;
            Destroy(gameObject);
        }
    }

}