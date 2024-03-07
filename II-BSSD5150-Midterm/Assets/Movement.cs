using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool canJump;
    private bool isDead = false;
    private bool canTouchGoal = true; // Flag to prevent repeated goal touches
    public Transform spawnPoint;
    private bool isDeadAnimation = false;
    public int health = 3; // Health variable

    private Rigidbody2D rb2d;
    private Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

            if (moveHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (moveHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            animator.SetBool("Walking", Mathf.Abs(moveHorizontal) > 0);
            animator.SetBool("Jumping", !canJump);

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
        }

        //Turn on code so no one can hurt you
        if (!isDeadAnimation && other.CompareTag("Test"))
        //if (!isDeadAnimation && other.CompareTag("Out"))
        {
            isDeadAnimation = true;
            health--; // Decrease health

            if (health <= 0)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                StartCoroutine(Respawn());
            }
        }

        if (canTouchGoal && other.CompareTag("Goal"))
        {
            // Prevent repeated touches by setting the flag
            canTouchGoal = false;

            // Determine the next level based on the current scene name
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (currentSceneName == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (currentSceneName == "Level3")
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }


    IEnumerator Respawn()
    {
        isDead = true;
        isDeadAnimation = true;
        rb2d.velocity = Vector2.zero; // Stop player movement
        rb2d.gravityScale = 0; // Stop gravity

        // Set animator parameters
        animator.SetBool("Dead", true);

        // Set a delay for respawn (you can adjust the duration)
        yield return new WaitForSeconds(0.95f);

        canJump = false;
        // Reset the position and state
        transform.position = spawnPoint.position;
        isDead = false;
        isDeadAnimation = false;
        rb2d.gravityScale = 1;
        animator.SetBool("Dead", false);
        animator.SetBool("Idle", true);// Reset Dead animation parameter
    }

    IEnumerator GameOver()
    {
        isDead = true;
        isDeadAnimation = true;
        rb2d.velocity = Vector2.zero; // Stop player movement
        rb2d.gravityScale = 0; // Stop gravity

        // Set animator parameters
        animator.SetBool("Dead", true);

        yield return new WaitForSeconds(0.95f); // Adjust the duration as needed

        SceneManager.LoadScene("GameOverScene");
    }
}

