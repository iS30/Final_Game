using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI YourScore;
    public Button restartButton;
    public Button quitButton;
    
    public GameObject titleScreen;

    public bool isGameActive;
    private int score;
    private bool isGamePaused = false;
    public float spawnRate = 2.0f;
    //private float startDelay = 1.5f;

    public SpawningManager spawnManager;

    // Start is called before the first frame update
    void Start () {

    }

    //to update the scoring system
    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    //to start the game
    public void StartGame(int difficulty) {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        //spawnManager.SpawnObstacle();
        StartCoroutine(spawnManager.SpawnCollectable());
        StartCoroutine(spawnManager.SpawnObstacle());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    //to end the game
    public void GameOver() {
        isGameActive = false;
        isGamePaused = true;
        GameOverText.gameObject.SetActive(true);
        YourScore.gameObject.SetActive(true);
        YourScore.SetText("You Got: " + score);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update() {
        //pause game
        if (isGamePaused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}