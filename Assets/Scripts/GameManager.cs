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
    
    private Canvas canvas;
    public Text highscoreText;

    private float highscoreCounter;
    private float multiplier;
    
    private void Start()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
        
        canvas = GetComponentInChildren<Canvas>();
        multiplier = 4;

        canvas.enabled = false;
        Time.timeScale = 1;
    }
    
    private void Update()
    {
        switch(gameState)
        {
            case GameState.MainMenu:
                if (SceneManager.GetActiveScene().name == "Game")
                    gameState = GameState.Loading;
                break;
            case GameState.Loading:
                if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == "Game")
                    gameState = GameState.Game;
                else if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == "MainMenu")
                    gameState = GameState.MainMenu;
                break;
            case GameState.Game:
                highscoreCounter += Time.deltaTime * multiplier;
                highscoreText.text = "Highscore: " + Mathf.RoundToInt(highscoreCounter);

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    canvas.enabled = true;
                    Time.timeScale = 0;
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    canvas.enabled = false;
                    Time.timeScale = 1;
                    gameState = GameState.Game;
                }
                break;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void StartGame()
    {
        Application.LoadLevel("Game");
    }
    
    public static GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}