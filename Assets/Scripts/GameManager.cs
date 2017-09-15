using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    MainMenu,
    Loading,
    Game,
    Pause,
    ChangeScene
}

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public GameState gameState;

    private Canvas pauseCanvas, mmCanvas;
    public Canvas reticleCanvas;
    public Text scoreText;
    public Text pauseHighscoreText;
    public Text mainMenuHighscoreText;
    public Transform player;
    public GameObject[] enemies;
    public GameObject[] enemySpawns;
    public Text debugText;
    public bool load;

    EnemySniper temp;

    private float scoreCounter;
    private float savedHighscoreCounter;
    private float multiplier;
    private int counter;
    
    private void Start()
    {

        savedHighscoreCounter = PlayerPrefs.GetInt("Highscore");

        Screen.orientation = ScreenOrientation.Landscape;

        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }

        pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        reticleCanvas = GameObject.Find("ReticleCanvas").GetComponent<Canvas>();
        mmCanvas = GameObject.Find("MenuCanvas").GetComponent<Canvas>();

        pauseCanvas.enabled = false;
        reticleCanvas.enabled = false;

        mainMenuHighscoreText.text = "Highscore: " + Mathf.RoundToInt(savedHighscoreCounter);

        multiplier = 4;

        Time.timeScale = 1;
    }
    
    private void Update()
    {
        switch(gameState)
        {
            case GameState.MainMenu:
                if (scoreCounter > 0)
                    scoreCounter = 0;

                if (SceneManager.GetActiveScene().name == "Game")
                    if (!mmCanvas.enabled)
                        mmCanvas.enabled = true;

                    gameState = GameState.Loading;
                break;
            case GameState.Loading:
                if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == "Game")
                {
                    load = true;
                    gameState = GameState.Game;
                }
                else if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == "MainMenu")
                    gameState = GameState.MainMenu;
                break;
            case GameState.Game:
                if (Time.timeScale <= 0 || load)
                    Time.timeScale = 1;

                if (!player || load)
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

                if (enemies == null || load)
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemySpawns == null || load)
                    enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawns");

                scoreCounter += Time.deltaTime * multiplier;
                scoreText.text = "Score: " + Mathf.RoundToInt(scoreCounter);

                if (!reticleCanvas.enabled)
                    reticleCanvas.enabled = true;

                if (mmCanvas.enabled)
                    mmCanvas.enabled = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (scoreCounter > savedHighscoreCounter)
                    {
                        pauseHighscoreText.text = "Highscore: " + Mathf.RoundToInt(scoreCounter);
                    }
                    else
                        pauseHighscoreText.text = "Highscore: " + Mathf.RoundToInt(savedHighscoreCounter);
                    reticleCanvas.enabled = false;
                    pauseCanvas.enabled = true;
                    Time.timeScale = 0;
                    gameState = GameState.Pause;
                }
                load = false;
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    reticleCanvas.enabled = true;
                    pauseCanvas.enabled = false;
                    Time.timeScale = 1;
                    gameState = GameState.Game;
                }
                break;
        }
    }
    
    public void QuitGame()
    {
        if (scoreCounter > savedHighscoreCounter)
        {
            PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(scoreCounter));
        }
        Application.Quit();
    }

    public void MainMenu()
    {
        if (scoreCounter > savedHighscoreCounter)
        {
            PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(scoreCounter));
        }
        gameState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void RespawnEnemy()
    {
        counter++;
        debugText.text = "Bang" + counter.ToString();
    }

    public static GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}