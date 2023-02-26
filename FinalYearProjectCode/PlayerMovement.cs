using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float xAxis;
    public float speed = 30f;
    public float jumpSpeed = 50f;
    private int maxJumps = 2;
    private int jumpsLeft;
    
    public static int coinCounter = 0;
    public static int livesCounter = 3;

    public float bulletSpeed = 20f;
    public int bulletDamage = 10;
    
    public TextMeshProUGUI coinOutput;
    public TextMeshProUGUI livesOutput;

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private GameObject playerSpawn;
    private GameObject projectileSpawner;
    private Animator animator;

    private bool grounded;
    private int direction = 1;

    public static bool gameOver;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    public GameObject bulletPrefab;
    public static bool keyObtained;

    [SerializeField] private LayerMask floorLayer;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        playerSpawn = GameObject.Find("PlayerSpawn");
        projectileSpawner = GameObject.Find("ProjectileSpawner");

        rigidBody.transform.position = playerSpawn.transform.position; // Sets the player location to player spawn

        gameOver = false;
        keyObtained = false;

        coinCounter = 0; //Reset the Coin Counter
        livesCounter = 3; //Resets the Lives Counter
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update() { 
        coinOutput.text = coinCounter + ""; // Renders current coins
        livesOutput.text = livesCounter + ""; // Renders current lives

        if (gameOver) {
            gameOverScreen.SetActive(true); //Display Game Over Screen
        } else if (keyObtained == true){
            winScreen.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1")){ // Mouse button 1
            // Create a new bullet at the position and rotation of the player
            GameObject bullet = Instantiate(bulletPrefab, projectileSpawner.transform.position, projectileSpawner.transform.rotation);

            // Get the bullet controller component from the bullet
            Bullet spawnedBullet = bullet.GetComponent<Bullet>();

            // Set the speed and damage of the bullet
            spawnedBullet.speed = bulletSpeed;
            spawnedBullet.damage = bulletDamage;
            spawnedBullet.direction = direction;
        }
    }
    
    void FixedUpdate() {
        grounded = isOnTheGround();
        xAxis = Input.GetAxis("Horizontal");

        // Setting up player movememnt so it can be manipulated
        Vector3 movement = new Vector3(xAxis * speed, rigidBody.velocity.y, 0);
        rigidBody.velocity = movement;

        // Allows player to jump:
        // if (Input.GetKey(KeyCode.Space))  {
        //     if (grounded && jumpsLeft > 0) {
        //         rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0);
        //     } 
        // }

         if (Input.GetKey(KeyCode.Space) && (jumpsLeft > 0)) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0);
            jumpsLeft -= 1;
        }

        if (grounded && rigidBody.velocity.y <= 0) {
            jumpsLeft = maxJumps;
        }
        
        // Allows player to change direction in appearance
        if (xAxis < 0f) {
            direction = -1;
            animator.SetBool("isWalking", grounded);
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
        } else if (xAxis > 0f) {
            direction = 1;
            animator.SetBool("isWalking", grounded);
            transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        } else
            animator.SetBool("isWalking", false);
        
        Physics.gravity = new Vector3(0, -100F, 0); // Overrides gravity of player

        if (numOfLives() <= 0) {
            rigidBody.velocity = Vector3.zero; //Stop player from moving
            gameOver = true; 
        }
    }

    // Checks if the player is on the ground using Raycast
    private bool isOnTheGround() {
        RaycastHit raycastHit;
        bool Raycast = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, out raycastHit, 5f, floorLayer);
        return Raycast;
    }

    // Decreases lives counter
    public static void decrementLives() {
        livesCounter--; 
    }

    public static void increaseLives() {
        livesCounter++;
    }

    public static int numOfLives() {
        return livesCounter; 
    }

    //Replay Current Level
    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); 
    }

    public void exitGame() {
        Application.Quit();
        print("Exited Game");
    }
}
