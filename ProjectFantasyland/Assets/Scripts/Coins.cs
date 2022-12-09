using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    private void OnTriggerEnter(Collider collide) {
        if (collide.transform.tag == "Player") {
            PlayerMovement.coinCounter++;
            Destroy(gameObject);
        }
    }

}