using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float offset; // Offset for the camera
    public float offsetSmoothing; // Smoothing for camera movement
    private Vector3 playerPosition; // To store the calculated camera position

    public float minX; // Minimum X boundary for the camera
    public float maxX; // Maximum X boundary for the camera

    void Start()
    {
        // Ensure player is assigned
        if (player == null)
        {
            Debug.LogError("Player not assigned to CameraControl.");
        }
    }

    void Update()
    {
        if (player == null) return; // If no player, do nothing

        // Base player position for the camera
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        // Adjust camera's X position based on the player's facing direction
        if (player.transform.localScale.x > 0f)
        {
            playerPosition.x += offset;
        }
        else if (player.transform.localScale.x < 0f)
        {
            playerPosition.x -= offset;
        }

        // Clamp the X position to the specified boundaries
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);

        // Smoothly move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
