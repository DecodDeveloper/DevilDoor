using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    // Reference to the Obstacle object
    public GameObject obstacleObject;
    private Obstacle obstacleScript;

    void Start()
    {
        // Get the Obstacle script from the Obstacle object
        if (obstacleObject != null)
        {
            obstacleScript = obstacleObject.GetComponent<Obstacle>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the GameObject has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the Obstacle script is attached
            if (obstacleScript != null)
            {
                // Update the canMove boolean to true
                obstacleScript.canMove = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the GameObject has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the Obstacle script is attached
            if (obstacleScript != null)
            {
                // Update the canMove boolean to false
                obstacleScript.canMove = false;
            }
        }
    }
}
