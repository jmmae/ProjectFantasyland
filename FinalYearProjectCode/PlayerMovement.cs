using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float xAxis;
    public float speed = 30f;
    public float runSpeed = 50f;

    public float jumpSpeed = 50f;
    public float secondJump = 30f;
    private bool spaceBarHeld = false;

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
    public GameObject pauseScreen;

    public GameObject bulletPrefab;
    public static bool keyObtained;

    private float debounce = 0.1f; 
    private float debounceTimer = 0f; 

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
        Time.timeScale = 1;
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
        debounceTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && (jumpsLeft > 0) && (debounceTimer <= 0) && (!spaceBarHeld)) {
            if (jumpsLeft == 1) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + secondJump, 0);
            } else {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y + jumpSpeed, 0);
            }
            jumpsLeft -= 1;
            debounceTimer = debounce;
            spaceBarHeld = true;
        } else if (!Input.GetKey(KeyCode.Space)) {
            spaceBarHeld = false;
        }

        if (grounded && rigidBody.velocity.y <= 0.05f && rigidBody.velocity.y >= -0.05f) {
            jumpsLeft = maxJumps;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            // Setting up player movememnt so it can be manipulated when LeftShift is held
            Vector3 movement = new Vector3(xAxis * runSpeed, rigidBody.velocity.y, 0);
            rigidBody.velocity = movement;
        } else {
            // Setting up player movememnt so it can be manipulated
            Vector3 movement = new Vector3(xAxis * speed, rigidBody.velocity.y, 0);
            rigidBody.velocity = movement;
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

    public void Resume() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause() {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void startMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void exitGame() {
        Application.Quit();
        print("Exited Game");
    }
}

