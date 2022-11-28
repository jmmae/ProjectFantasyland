using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
    
    public int direction = 1;
    public float distance = 5;
    public float speed = 5f;
    private float startingPos;

    void Start() {
        startingPos = transform.position.x;
    }

    void FixedUpdate() {
        transform.Translate(Vector2.right * Time.deltaTime * speed * direction); //Move the enemy to the right
        if (transform.position.x > startingPos + distance || transform.position.x < startingPos) 
            direction *= -1; //Change direction of sprite
    }
}
