using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private void OnTriggerEnter(Collider collide) {
        if (collide.transform.tag == "Player") {
            PlayerMovement.increaseLives();
            Destroy(gameObject);
        }
    }
}