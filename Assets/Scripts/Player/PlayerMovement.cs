using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.SceneManagement; // For restarting the level

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementConfig[] movementConfigs;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.2f;

    [SerializeField]
    private ParticleSystem dustParticleSystem; // Reference to the dust particle system

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool moveLeft;
    private bool moveRight;

    public bool playerMove = false;

    // Current movement settings
    private float moveSpeed;
    private float jumpForce;
    private float gravityScale;
    private float airControlMultiplier;

    private Animator animator;

    // New flag to track death state
    private bool isDead = false;
    
    private Vector3 respawnPoint; // Last checkpoint position
    public GameObject fallDetector;


    public int hearts = 3; // Total hearts
    public GameObject gameOverScreen; // Reference to the Game Over UI
    public Text heartsText; // Text to display the heart count
     public Text LifeTxt; // Text to display the Life count


    public bool MoveLeft
    {
        get { return moveLeft; }
        set
        {   
            moveLeft = value;
            UpdateHorizontalInput();
        }
    }

    public bool MoveRight
    {
        get { return moveRight; }
        set
        {
            moveRight = value;
            UpdateHorizontalInput();
        }
    }

    public bool Grounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyMovementConfig(transform.localScale.x);
        rb.gravityScale = gravityScale;
        animator = GetComponent<Animator>();

        respawnPoint = transform.position; // respawn player

         UpdateHeartsUI(); // Update hearts display at start
         gameOverScreen.SetActive(false); // Ensure Game Over screen is hidden initially

        LifeTxt.text = "Life: " + hearts; 
    }

    private void Update()
    {
        if (isDead) return; // Prevent any further updates if dead

        UpdateHorizontalInput();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded); // Update animator with grounded status

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            CreateDust(); // Create dust when jumping
        }

        HandleAnimations(); // Ensure animations are handled in every update

        // Create dust while moving
        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f)
        {
            CreateDust();
        }
        else
        {
            StopDust(); // Stop dust when not moving
        }
         fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y); // re spawn player position
    }

    private void FixedUpdate()
    {
        if (isDead) return; // Prevent any further physics updates if dead

        ApplyHorizontalMovement();
        ApplyGravityModification();
    }
      private void OnTriggerEnter2D(Collider2D collision) // collision method for respawn
    {

        if(collision.tag == "FallDetector")
        {
            Dead(); // Handle player death
        }
        else if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }

     void Dead() // dead method to handle game over and hearts update
    {
        hearts--; // Decrease heart count
         LifeTxt.text = "Life: " + hearts; 

        UpdateHeartsUI(); // Update the UI

        if (hearts <= 0)
        {
            GameOver(); // Trigger Game Over if no hearts remain
        }
        else
        {
            transform.position = respawnPoint; // Respawn at the last checkpoint
        }
    }

      void GameOver() // to acitve the game over screen;
    {
        gameOverScreen.SetActive(true); // Show the Game Over screen
        Time.timeScale = 0; // Pause the game
    }


    public void RestartLevel() // for restart the level 
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void WatchAdToRestoreHearts() // for watch ad
    {
        Debug.Log("Playing ad..."); // Placeholder for ad integration
        RestoreHearts(); // Restore hearts after ad
    }
    
    void RestoreHearts()
    {
        hearts = 3; // Restore all hearts
        UpdateHeartsUI(); // Update the UI
        gameOverScreen.SetActive(false); // Hide Game Over screen
        Time.timeScale = 1; // Unpause the game
    }
     void UpdateHeartsUI()
    {
        heartsText.text = "Hearts: " + hearts; // Update the hearts count in the UI
    }

    private void UpdateHorizontalInput()
    {
        if (moveLeft || moveRight)
        {
            horizontalInput = moveLeft ? -1 : 1;
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            horizontalInput = 0;
             playerMove = true;
        }
    }

    private void ApplyHorizontalMovement()
    {
        float effectiveMoveSpeed = isGrounded ? moveSpeed : moveSpeed * airControlMultiplier;
        rb.velocity = new Vector2(horizontalInput * effectiveMoveSpeed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void ApplyGravityModification()
    {
        if (!isGrounded)
        {
            rb.velocity +=
                Vector2.up * Physics2D.gravity.y * (gravityScale - 1) * Time.fixedDeltaTime;
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump"); // Trigger the jump animation
    }

    // New method to handle player death
    public void Die()
    {
        if (isDead) return; // If already dead, no need to execute further
        
        isDead = true;  // Set dead flag
        rb.velocity = Vector2.zero; // Stop any movement
        rb.gravityScale = 0; // Disable gravity to prevent falling

        animator.SetTrigger("Dead"); // Trigger death animation

        StopDust(); // Stop any particle effects

        // Optionally: Disable collider or make the player unresponsive
        // GetComponent<Collider2D>().enabled = false;
    }

    private void HandleAnimations()
    {
        if (isDead)
        {
            animator.Play("Dead"); // Play dead animation if dead
        }
        else if (!isGrounded)
        {
            animator.Play("Jump"); // Ensure the jump animation plays while in the air
        }
        else if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    private void ApplyMovementConfig(float scale)
    {
        foreach (var config in movementConfigs)
        {
            if (Mathf.Approximately(scale, config.scale))
            {
                moveSpeed = config.moveSpeed;
                jumpForce = config.jumpForce;
                gravityScale = config.gravityScale;
                airControlMultiplier = config.airControlMultiplier;
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void CreateDust()
    {
        if (dustParticleSystem != null && !dustParticleSystem.isPlaying)
        {
            dustParticleSystem.Play();
        }
    }

    private void StopDust()
    {
        if (dustParticleSystem != null && dustParticleSystem.isPlaying)
        {
            dustParticleSystem.Stop();
        }
    }

    // // Detect collisions to trigger death (replace "Obstacle" with your actual tag)
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Obstacle")) // Replace "Obstacle" with the actual tag
    //     {
    //         Die(); // Call Die method when colliding with an obstacle
    //     }
    // }
}

[System.Serializable]
public class PlayerMovementConfig
{
    public float scale;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float airControlMultiplier;
}
