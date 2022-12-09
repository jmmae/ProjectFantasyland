using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
    
    public int direction = 1;
    public int health = 100; //Enemy Health stat
    public float distance = 5;
    public float speed = 5f;
    public float debounce = 2f; //Number in seconds for hit debounce (prevents instant death)
    private float debounceTimer = 0; //Internal number to increase and decrease (custom timer)
    private float startingPos;

    void Start() {
        startingPos = transform.position.x;
    }

    // Negate time elapsed since last physics update
    void FixedUpdate() {
        debounceTimer -= Time.deltaTime;

        transform.Translate(Vector3.right * Time.deltaTime * speed * direction); //Move the enemy to the right
        if (transform.position.x > startingPos + distance || transform.position.x < startingPos) 
            direction *= -1; //Change direction of sprite
    }

    //Checks if the player is colliding with an enemy and if the debounce timer isn't running
    private void OnTriggerStay(Collider collide) { // OnTriggerStay runs as long as something is colliding
        if (collide.transform.tag == "Player" && debounceTimer <= 0) {
            debounceTimer = debounce;
            PlayerMovement.decrementLives(); // Decreases lives by 1 when hit
        }
    }

    public void TakeDamage(int Damage) {
        health -= Damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}