using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    private int score;
    private int lives; 
    private int gameDifficulty;
    public float spawnRate = 2.0f;
    public bool isGameActive = true;
    public Button pauseButton;
    public GameObject titleScreen;
    public GameObject pauseMenu;
    public GameObject GameOverScreen;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI titleScreenHighScoreText;
    public TextMeshProUGUI gameOverScreenHighScoreText;
    public TextMeshProUGUI pauseMenuHighScoreText;

    // Start is called before the first frame update
    void Start()
    {   
        titleScreenHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget()
    {   
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToReduce)
    {
        if(isGameActive)
        {
            lives -= livesToReduce;
            livesText.text = "Lives: " + lives;

            if(lives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {  
        GameOverScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        isGameActive = false;

        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            gameOverScreenHighScoreText.text = "New High Score: " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            gameOverScreenHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void RestartGame()
    {   
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseMenuHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void StartGame(int difficultyLevel)
    {
        score = 0;
        lives = 3;
        gameDifficulty = difficultyLevel; 
        UpdateScore(0);
        UpdateLives(0);
        spawnRate /= difficultyLevel;
        StartCoroutine(SpawnTarget());

        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }
}
