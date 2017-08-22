using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    #region Variables
    public Transform player;

    private float minDistance;
    private float maxDistance;

    private float minHeight;
    private float maxHeight;

    private int powerlineCounter;
    private int groundCounter;
    
    private GameObject[] powerlinesToSpawn;
    private Transform powerlineTrigger;

    private GameObject[] groundsToSpawn;
    private Transform groundTrigger; 

    private Vector3 powerlineSpaces = new Vector3(149, 0, 0);
    private Vector3 groundSpaces = new Vector3(90, 0, 0);
    #endregion

    private void Start()
    {
        if (!powerlineTrigger)
            powerlineTrigger = GameObject.FindGameObjectWithTag("PowerlineTrigger").GetComponent<Transform>();

        if (!groundTrigger)
            groundTrigger = GameObject.FindGameObjectWithTag("GroundTrigger").GetComponent<Transform>();

        powerlinesToSpawn = GameObject.FindGameObjectsWithTag("Powerlines");
        groundsToSpawn = GameObject.FindGameObjectsWithTag("Grounds");

        for (int i = 0; i < powerlinesToSpawn.Length; i++)
        {
            powerlinesToSpawn[i].transform.position = i * powerlineSpaces;
        }

        for (int i = 0; i < powerlinesToSpawn.Length; i++)
        {
            groundsToSpawn[i].transform.position = i * groundSpaces;
        }

        powerlineCounter = 0;
        groundCounter = 0;
    }

    private void Update()
    {
        if(player.transform.position.x >= powerlineTrigger.position.x)
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
        } ///////////////////POWERLINE SPAWNER

        if(player.transform.position.x >= groundTrigger.position.x)
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
        } ///////////////////GROUND SPAWNER
    }
}