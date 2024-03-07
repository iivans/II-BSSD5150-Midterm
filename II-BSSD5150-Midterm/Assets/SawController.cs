using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    public float speed = 5f;
    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAreaBoundary"))
        {
            // Change direction when touching the boundary
            movingRight = !movingRight;

            // Flip the enemy sprite horizontally
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}