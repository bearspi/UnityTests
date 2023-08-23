using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Vector2 destination;
    public float moveSpeed = 5.0f;

    private bool movingToDestination = true;
    private Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }
    void FixedUpdate()
    {   
        Vector2 targetPosition = movingToDestination ? destination : startingPosition;
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 nextPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        transform.position = nextPosition;

        // Check if the object has reached the target position
        if (nextPosition == targetPosition)
        {
            movingToDestination = !movingToDestination; // Toggle the state
        }
    }
}
