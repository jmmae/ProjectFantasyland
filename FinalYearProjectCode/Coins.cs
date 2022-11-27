using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collide) {
        if (collide.transform.tag == "Player") {
            PlayerMovement.coinCounter++;
            Destroy(gameObject);
        }
    }

}
