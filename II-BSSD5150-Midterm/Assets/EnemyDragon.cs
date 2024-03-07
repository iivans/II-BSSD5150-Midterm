using System.Collections;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float speed = 5f;
    private bool movingRight = true;
    private bool isIdle = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isIdle)
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
    }

    void MoveRight()
    {
        animator.SetBool("Walking", true);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void MoveLeft()
    {
        animator.SetBool("Walking", true);
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAreaBoundary"))
        {
            StartCoroutine(RandomAction());
            // Change direction when touching the boundary
            movingRight = !movingRight;

            // Flip the enemy sprite horizontally
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            

        }
    }

    IEnumerator RandomAction()
    {
        // Freeze movement
        isIdle = true;

        // Decide whether to perform a fireball or idle
        if (Random.Range(0f, 1f) < 0.5f)
        {
            // Fireball action
            ShootFireball();
        }
        if (Random.Range(0f, 1f) < 0.5f)
        {
            // Idle action
            yield return StartCoroutine(RandomIdle());
        }

        // Unfreeze movement
        isIdle = false;
    }

    void ShootFireball()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);

        // Instantiate a fireball prefab at the enemy's position
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Set the fireball's direction based on the enemy's facing direction
        FireballController fireballController = fireball.GetComponent<FireballController>();
        if (fireballController != null)
        {
            fireballController.SetDirection(movingRight ? Vector2.right : Vector2.left);
        }

        // Destroy the fireball after a certain time
        Destroy(fireball, 3.6f);
        animator.SetBool("Attack", false);
    }

    IEnumerator RandomIdle()
    {
        // Set idle to true
        isIdle = true;
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", true);

        // Wait for a random duration
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        animator.SetBool("Idle", false);
        // Set idle to false
        isIdle = false;
    }
}
