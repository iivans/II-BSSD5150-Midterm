using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public float gameTime = 60f; // Set the time limit in seconds



    void Start()
    {
        StartCoroutine(Countdown());
    }

    void Update()
    {

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(gameTime);

        // Game over, switch to the game over scene
        SceneManager.LoadScene("GameOverScene");

    }


}