using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    #region Variables
    GameManager gm;

    // Powerline, obstacle and ground to spawn
    private int powerlineCounter;
    private int groundCounter;
    private int obstacleCounter;
    private int previousObstacle;

    // Powerline trigger and objects to spawn
    private GameObject[] powerlinesToSpawn;
    private Transform powerlineTrigger;

    // Ground trigger and objects to spawn
    private GameObject[] groundsToSpawn;
    private Transform groundTrigger;

    // Obstacle trigger and objects to spawn
    private GameObject[] obstacleToSpawn;
    private Transform obstacleTrigger;

    // Default spacing for ground and powerline spawning
    private Vector3 powerlineSpaces = new Vector3(149, 0, 0);
    private Vector3 groundSpaces = new Vector3(90, 0, 0);
    private Vector3 obstacleSpaces = new Vector3(40, 0, 0);
    #endregion

    private void Start()
    {
        if (!powerlineTrigger)
            powerlineTrigger = GameObject.FindGameObjectWithTag("PowerlineTrigger").GetComponent<Transform>();

        if (!groundTrigger)
            groundTrigger = GameObject.FindGameObjectWithTag("GroundTrigger").GetComponent<Transform>();

        if (!obstacleTrigger)
            obstacleTrigger = GameObject.FindGameObjectWithTag("ObstacleTrigger").GetComponent<Transform>();

        powerlinesToSpawn = GameObject.FindGameObjectsWithTag("Powerlines");
        groundsToSpawn = GameObject.FindGameObjectsWithTag("Grounds");
        obstacleToSpawn = GameObject.FindGameObjectsWithTag("Obstacle");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < powerlinesToSpawn.Length; i++)
        {
            powerlinesToSpawn[i].transform.position = i * powerlineSpaces;
            groundsToSpawn[i].transform.position = i * groundSpaces;
            obstacleToSpawn[i].SetActive(false);
        }

        powerlineCounter = 0;
        groundCounter = 0;
        obstacleCounter = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.player != null)
        {
            switch (gm.gameState)
            {
                case GameState.Game:
                    ///////////////////POWERLINE SPAWNER
                    if (GameManager.Instance.player.position.x >= powerlineTrigger.position.x)
                    {
                        if (powerlineCounter < powerlinesToSpawn.Length)
                        {
                            powerlinesToSpawn[powerlineCounter].transform.position += 3 * powerlineSpaces;
                            powerlineTrigger.transform.position += powerlineSpaces;
                            powerlineCounter++;
                        }
                        else
                        {
                            powerlineCounter = 0;
                            powerlinesToSpawn[powerlineCounter].transform.position += 3 * powerlineSpaces;
                            powerlineTrigger.transform.position += powerlineSpaces;
                            powerlineCounter++;
                        }
                    }
                    ///////////////////GROUND SPAWNER
                    if (GameManager.Instance.player.position.x >= groundTrigger.position.x)
                    {
                        if (groundCounter < powerlinesToSpawn.Length)
                        {
                            groundsToSpawn[groundCounter].transform.position += 3 * groundSpaces;
                            groundTrigger.transform.position += groundSpaces;
                            groundCounter++;
                        }
                        else
                        {
                            groundCounter = 0;
                            groundsToSpawn[groundCounter].transform.position += 3 * groundSpaces;
                            groundTrigger.transform.position += groundSpaces;
                            groundCounter++;
                        }
                    }
                    ///////////////////OBSTACLE SPAWNER
                    //if (GameManager.Instance.player.position.x >= obstacleTrigger.position.x)
                    //{
                    //    previousObstacle = obstacleCounter;

                    //    obstacleToSpawn[previousObstacle].SetActive(false);

                    //    while (obstacleCounter == previousObstacle)
                    //    {
                    //        obstacleCounter = Random.Range(0, 6);
                    //    }

                    //    obstacleToSpawn[obstacleCounter].SetActive(true);

                    //    obstacleToSpawn[obstacleCounter].transform.position = (GameManager.Instance.player.position - new Vector3(0, GameManager.Instance.player.position.y, 0)) + obstacleSpaces;
                    //    obstacleTrigger.transform.position += (1.5f * obstacleSpaces);
                    //}
                    break;
                case GameState.Pause:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.Loading:
                    break;
            }
        } 
    }
}