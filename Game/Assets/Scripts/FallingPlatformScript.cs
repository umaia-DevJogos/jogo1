using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    // The speed at which the platform falls
    public float fallSpeed = 1.0f;

    // The Y level at which the platform should stop falling
    public float desiredYLevel = 0.0f;

    // A flag to track if the platform has fallen
    private bool hasFallen = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the platform has already fallen, return
        if (hasFallen) return;

        // If the collision is with the player character, start the falling sequence
        if (collision.gameObject.tag == "Player")
        {
            // Set the flag to indicate that the platform has fallen
            hasFallen = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasFallen) return;

        if (collision.gameObject.tag == "Player")
        {
            hasFallen = true;
        }
    }

    void Update()
    {
        // If the platform has not fallen, return
        if (!hasFallen) return;

        // Calculate the distance to fall based on the speed and delta time
        float distance = fallSpeed * Time.deltaTime;

        // Calculate the new position of the platform
        Vector3 newPosition = transform.position;
        newPosition.y -= distance;

        // Check if the platform has reached the desired Y level
        if (newPosition.y <= desiredYLevel)
        {
            // Set the platform's Y position to the desired Y level and stop the falling
            newPosition.y = desiredYLevel;
            transform.position = newPosition;
            return;
        }

        // Set the platform's new position
        transform.position = newPosition;
    }
}
