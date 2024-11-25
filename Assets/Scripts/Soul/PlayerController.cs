using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the right arrow key is being pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Play the run animation directly
            animator.Play("Run");
        }

        // Optionally, stop the animation or transition to idle when the key is released
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // Play the idle animation or another appropriate animation
            animator.Play("Idle");
        }
    }
}
