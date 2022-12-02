using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {

    public float xAxis;
    public float speed = 30f;
    public float jumpSpeed = 50f;
    
    public static int coinCounter = 0;
    public static int livesCounter = 3;
    
    public TextMeshProUGUI coinOutput;
    public TextMeshProUGUI livesOutput;

    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private LayerMask floorLayer;

    private GameObject cameraMovement;
    private GameObject playerSpawn;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        cameraMovement = GameObject.Find("Main Camera");
        playerSpawn = GameObject.Find("PlayerSpawn");

        rigidBody.transform.position = playerSpawn.transform.position; // Sets the player location to player spawn
    }

    // Update is called once per frame
    void Update() { 
        coinOutput.text = "Coins: " + coinCounter; // Renders current coins
        livesOutput.text = "Lives: " + livesCounter; // Renders current lives
    }
    
    void FixedUpdate() {
        xAxis = Input.GetAxis("Horizontal");

        // Setting up player movememnt so it can be manipulated
        Vector3 movement = new Vector3(xAxis * speed, rigidBody.velocity.y, 0);
        rigidBody.velocity = movement;

        // Allows player to jump:
        if(isOnTheGround() && Input.GetKey(KeyCode.Space))
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);

        spriteRenderer.flipX = (xAxis < -0f); // Allows player to change direction in appearance

        cameraMovement.transform.position = new Vector3(rigidBody.position.x, rigidBody.position.y, cameraMovement.transform.position.z) + new Vector3(0, 20, 0); //Camera follows player
        
        Physics.gravity = new Vector3(0, -100F, 0); // Overrides gravity of player
    }

    // Checks if the player is on the ground using Raycast
    private bool isOnTheGround() {
        RaycastHit raycastHit;
        bool Raycast = Physics.BoxCast(boxCollider.bounds.center + new Vector3(0,3,0), boxCollider.bounds.size, Vector3.down, out raycastHit, transform.rotation, 2f, floorLayer);
        return Raycast;
    }

    // Decreases lives counter
    public static void decrementLives() {
        // if(livesCounter > 0f) 
        livesCounter--; 
        //     rigidBody.transform.position = playerSpawn.transform.position; 
        // else if(livesCounter <= 0f) 
        //     print("no more lives :(");
        //     rigidBody.transform.position = playerSpawn.transform.position;
    }

}
