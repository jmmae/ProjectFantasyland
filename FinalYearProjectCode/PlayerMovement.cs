using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {

    public float xAxis;
    public float speed = 30f;
    public float jumpSpeed = 20f;
    public static int coinCounter = 0;
    public TextMeshProUGUI coinOutput;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask floorLayer;

    private GameObject cameraMovement;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        cameraMovement = GameObject.Find("Main Camera");
    }

    //Update is called once per frame
    void Update() { 
        xAxis = Input.GetAxis("Horizontal");

        // Setting up player movememnt so it can be manipulated
        Vector2 movement = new Vector2(xAxis * speed, rigidBody.velocity.y);
        rigidBody.velocity = movement;

        // Allows player to jump:
        if(isOnTheGround() && Input.GetKey(KeyCode.Space))
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);

        // Allows player to change direction in appearance
        if(xAxis > 0f)
            transform.localScale = Vector3.one;
        else if (xAxis < -0f)
            transform.localScale = new Vector3(-1, 1, 1); //Flip sprite in direction it's moving in

        cameraMovement.transform.position = new Vector3(rigidBody.position.x, rigidBody.position.y, -20); //Camera follows player

        coinOutput.text = "Coins: " + coinCounter; //Increases the Coin Counter in the UI
    }
    
    void FixedUpdate() {
        rigidBody.rotation = 0f; //Limits rotation of sprite
    }

    // Checks if the player is on the ground using RaycastHit
    private bool isOnTheGround() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, floorLayer);
        return raycastHit.collider != null;
    }

}
