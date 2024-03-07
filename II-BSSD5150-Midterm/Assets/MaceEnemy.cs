using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceEnemy : MonoBehaviour
{
    public float speed = 5f;
    private bool movingUp = true;

    void Update()
    {
        if (movingUp)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyMaceBoundary"))
        {
            // Change direction when touching the boundary
            movingUp = !movingUp;
        }
    }
}