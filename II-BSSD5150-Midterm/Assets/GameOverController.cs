using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public string gamestart = "Level1"; // Change this to the name of your next level

    void Update()
    {
        if (Input.anyKeyDown)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        // Load the next level or reload the current level
        SceneManager.LoadScene(gamestart);
    }
}