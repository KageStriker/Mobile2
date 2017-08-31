﻿using System.Collections;
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
    
    private Canvas pauseCanvas, reticleCanvas, mmCanvas;
    public Text highscoreText;

    private float highscoreCounter;
    private float multiplier;
    
    private void Start()
    {
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

        multiplier = 4;

        Time.timeScale = 1;
    }
    
    private void Update()
    {
        switch(gameState)
        {
            case GameState.MainMenu:
                if (SceneManager.GetActiveScene().name == "Game")
                    if (!mmCanvas.enabled)
                        mmCanvas.enabled = true;

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

                if (!reticleCanvas.enabled)
                    reticleCanvas.enabled = true;

                if (mmCanvas.enabled)
                    mmCanvas.enabled = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseCanvas.enabled = true;
                    Time.timeScale = 0;
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    reticleCanvas.enabled = false;
                    pauseCanvas.enabled = false;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public static GameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}