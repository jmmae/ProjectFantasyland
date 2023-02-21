using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public int direction = 1;
    public float distance = 5;
    public float speed = 5f;
    private float startingPos;

    void Start() {
        startingPos = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.Translate(Vector3.down * Time.deltaTime * speed * direction); //Move the enemy to the right
        if (transform.position.y > startingPos + distance || transform.position.y < startingPos) 
            direction *= -1; //Change direction of sprite
    }
}
