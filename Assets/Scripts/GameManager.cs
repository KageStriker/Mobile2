using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    
    private Canvas canvas;
    public Text highscoreText;

    int highscoreCounter;
    float multiplier;
    
    // Use this for initialization
    void Start()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }


        canvas = GetComponentInChildren<Canvas>();
        multiplier = 4;

        canvas.enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            highscoreCounter += Mathf.RoundToInt(Time.deltaTime * multiplier);
            highscoreText.text = "Highscore: " + highscoreCounter;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenu" || Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game")
        {
            if (Time.timeScale == 0)
            {
                canvas.enabled = false;
                Time.timeScale = 1;
            }
            else
            {
                canvas.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Application.LoadLevel("GameOver");
    }

    public void StartGame()
    {
        Application.LoadLevel(1);
    }
    

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}