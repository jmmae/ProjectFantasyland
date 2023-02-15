using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private float despawn = 3; // Number in seconds until bullet despawn
    // The speed of the bullet
    public float speed = 20f;

    // The damage the bullet will deal
    public int damage = 1;

    public int direction = 1;

    // Update is called once per frame
    void Update() {
        despawn -= Time.deltaTime;
        if (despawn <= 0) {
            Destroy(gameObject);
        }

        // Move the bullet forward at the specified speed and re-orient
        transform.rotation = Quaternion.AngleAxis(90, -Vector3.forward);
        transform.position += transform.up * speed * Time.deltaTime * direction;
    }

    // Called when the bullet hits a collider
    private void OnTriggerStay(Collider collide) {
        // If it does, deal the specified damage to the health
        if (collide.tag == "Enemy") {
            collide.GetComponent<Enemies>().TakeDamage(damage);
            // Destroy the bullet
            Destroy(gameObject);
        }

        if (collide.tag == "Enemy2") {
            collide.GetComponent<Enemies2>().TakeDamage(damage);
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
