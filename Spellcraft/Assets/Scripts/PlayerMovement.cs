using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Animator animator; // Reference to Animator component

    private Vector2 movement; // To store player movement input

    // Initialize components in Start
    void Start()
    {
        // Automatically find the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing from this GameObject!");
        }
    }

    void Update()
    {
        // Get player input from WASD/Arrow keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement to avoid faster diagonal movement
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Move the character by changing the Transform's position
        Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
        transform.position = newPosition;


        // Check movement direction and set animation parameters
        if (movement.y > 0)
        {
            // Moving up
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Side", false);
        }
        else if (movement.y < 0)
        {
            // Moving down
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Side", false);
        }
        else if (movement.x != 0)
        {
            // Moving sideways
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Side", true);

            // Flip character based on movement direction
            if (movement.x > 0)
                transform.localScale = new Vector3(-2, 2, 2); // Facing right
            else if (movement.x < 0)
                transform.localScale = new Vector3(2, 2, 2); // Facing left
        }
        else
        {
            // If no movement, stop all animations
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Side", false);
        }
    }
}
