using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed = 5f;
    public float timeBeforeExplode = 3f; // Time before the fireball explodes
    private Rigidbody2D rb2d;

    private Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Invoke("Explode", timeBeforeExplode);
    }

    void Update()
    {
        // Move the fireball regardless of whether it's frozen or not
        MoveFireball();
    }

    void MoveFireball()
    {
        // Move the fireball
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Explode()
    {
        // Set the "Explode" parameter to true in the Animator
        animator.SetBool("Explode", true);

        // Destroy the fireball after some time 
        Destroy(gameObject, 3.6f);
    }

    public void SetDirection(Vector2 direction)
    {
        // Flip the sprite if moving left
        if (direction.x < 0)
        {
            // Assuming your sprite renderer is attached to the same GameObject
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }

            // Update the movement direction
            speed = -Mathf.Abs(speed);
        }
    }
}
