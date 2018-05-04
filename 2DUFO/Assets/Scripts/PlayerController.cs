using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidBody;
    [SerializeField] float playerSpeed = 10;
    
    private float score = 0;
    public Text scoreText;
    private float levelTimer = 20;
    public Text levelTimerText;
    public Text gameOverText;

    private bool gameActive = true;

    int pickupCount;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        pickupCount = GameObject.Find("Pickups").transform.childCount;
    }


    void FixedUpdate()
    {
        if(gameActive == true)
        {
            ProcessMovement();
            UpdateTimer();
        }
        
    }

    private void Update()
    {
        if(gameActive == false)
        {
            RestartGame();
        }
    }

    private void ProcessMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rigidBody.AddForce(movement * playerSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PickUp"))
        {
            Destroy(collision.gameObject);
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;

        if (score == pickupCount)
        {
            EndGame();
        }
    }

    private void UpdateTimer()
    {
        levelTimer -= Time.deltaTime;
        levelTimerText.text = "TIME: " + Mathf.Round(levelTimer).ToString("00");
        if(levelTimer < 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameActive = false;

        if (levelTimer > 0)
        {
            score *= Mathf.Round(levelTimer);
        }

        scoreText.gameObject.SetActive(false);
        levelTimerText.gameObject.SetActive(false);

        gameOverText.text = "GAME OVER\n\n\n\n" + "FINAL SCORE: " + score + "\n\n\n\nPress Enter to start again";
        gameOverText.gameObject.SetActive(true);

        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void RestartGame()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
