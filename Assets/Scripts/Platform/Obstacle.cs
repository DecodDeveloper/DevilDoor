using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 2f;

    // Maximum limits for movement
    public float maxLeft = -5f;
    public float maxRight = 5f;
    public float maxUp = 5f;
    public float maxDown = -5f;

    // Boolean to control whether the obstacle should move back (default to true)
    public bool moveBackEnabled = true;
    public bool resetPosition = false;

    // Boolean to control whether the obstacle can move (default to false)
    public bool canMove = false;

    // Original starting position
    private Vector3 startPosition;

    // Tracks if the platform should move back
    private bool moveBack = false;

    // Queue to manage movement directions
    private Queue<System.Action> movementQueue = new Queue<System.Action>();

    private bool isMoving = false;

    // List to configure the movement order
    public List<string> movementOrder = new List<string>();

    private void Start()
    {
        // Store the original position of the obstacle
        startPosition = transform.position;

        // Initialize the movement queue
        InitializeMovementQueue();
    }

    private void Update()
    {
        // When canMove is set to false, reset the position to the initial position
        if (!canMove)
        {
            if (resetPosition)
            {
                // Stop the movement and reset the position
                transform.position = startPosition;
                isMoving = false;
                // Clear any remaining movements in the queue
                movementQueue.Clear();
                // Reinitialize the movement queue for the next time canMove is set to true
                InitializeMovementQueue();
            }
            return;
        }

        // When canMove is set to true, start the movement sequence
        if (canMove && !isMoving && movementQueue.Count > 0)
        {
            // Execute the next movement in the queue
            isMoving = true;
            movementQueue.Dequeue().Invoke();
        }
    }

    private void InitializeMovementQueue()
    {
        foreach (var move in movementOrder)
        {
            switch (move.ToLower())
            {
                case "left":
                    movementQueue.Enqueue(() => MoveLeft());
                    break;
                case "right":
                    movementQueue.Enqueue(() => MoveRight());
                    break;
                case "up":
                    movementQueue.Enqueue(() => MoveUp());
                    break;
                case "down":
                    movementQueue.Enqueue(() => MoveDown());
                    break;
            }
        }
    }

    private void MoveLeft()
    {
        StartCoroutine(MoveToPosition(new Vector3(maxLeft, transform.position.y, transform.position.z), "moveLeft"));
    }

    private void MoveRight()
    {
        StartCoroutine(MoveToPosition(new Vector3(maxRight, transform.position.y, transform.position.z), "moveRight"));
    }

    private void MoveUp()
    {
        StartCoroutine(MoveToPosition(new Vector3(transform.position.x, maxUp, transform.position.z), "moveUp"));
    }

    private void MoveDown()
    {
        StartCoroutine(MoveToPosition(new Vector3(transform.position.x, maxDown, transform.position.z), "moveDown"));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, string movementType)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        // Reset the movement boolean and check if we need to move back
        isMoving = false;

        if (moveBackEnabled && movementQueue.Count == 0)
        {
            moveBack = true;
            ReturnToStartPosition();
        }
    }

    private void ReturnToStartPosition()
    {
        StartCoroutine(MoveBackToStart());
    }

    private IEnumerator MoveBackToStart()
    {
        while (Vector3.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = startPosition;
        moveBack = false;
    }
}
