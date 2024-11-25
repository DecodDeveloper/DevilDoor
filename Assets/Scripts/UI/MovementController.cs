using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        FindPlayerMovement();
    }

    void Update()
    {
        // Processing continuous input can be done here, if needed
    }

    // Finds the PlayerMovement component on the "Player" GameObject
    private void FindPlayerMovement()
    {
        // Attempt to find the player by tag instead of name for better reliability
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component not found on Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    public void OnLeftButtonDown()
    {
        if (playerMovement != null)
        {
            playerMovement.MoveLeft = true;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not initialized.");
        }
    }

    public void OnRightButtonDown()
    {
        if (playerMovement != null)
        {
            playerMovement.MoveRight = true;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not initialized.");
        }
    }

    public void OnJumpButtonDown()
    {
        if (playerMovement != null && playerMovement.Grounded)
        {
            playerMovement.Jump();
        }
        else
        {
            Debug.LogWarning("PlayerMovement not initialized.");
        }
    }

    public void OnLeftButtonUp()
    {
        if (playerMovement != null)
        {
            playerMovement.MoveLeft = false;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not initialized.");
        }
    }

    public void OnRightButtonUp()
    {
        if (playerMovement != null)
        {
            playerMovement.MoveRight = false;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not initialized.");
        }
    }
}
