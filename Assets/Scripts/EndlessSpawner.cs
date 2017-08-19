using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnItem
{
    Blockade,
    Overpass
}

public class EndlessSpawner : MonoBehaviour
{
    public Transform player;

    private float minDistance;
    private float maxDistance;
    private float minHeight;
    private float maxHeight;
    private int a;
    private int b;

    private SpawnItem item;

    private GameObject[] powerlinesToSpawn;
    private Transform powerlineTrigger;
    private GameObject[] groundsToSpawn;
    private Transform groundTrigger;

    private Vector3 powerlineSpaces = new Vector3(149, 0, 0);
    private Vector3 groundSpaces = new Vector3(90, 0, 0);

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

        a = 0;
        b = 0;
    }

    private void Update()
    {
        if(player.transform.position.x >= powerlineTrigger.position.x)
        {
            if (a < powerlinesToSpawn.Length)
            {
                powerlinesToSpawn[a].transform.position += 3 * powerlineSpaces;
                powerlineTrigger.transform.position += powerlineSpaces;
                a++;
            }
            else
            {
                a = 0;
                powerlinesToSpawn[a].transform.position += 3 * powerlineSpaces;
                powerlineTrigger.transform.position += powerlineSpaces;
                a++;
            }
        }

        if(player.transform.position.x >= groundTrigger.position.x)
        {
            if (b < powerlinesToSpawn.Length)
            {
                groundsToSpawn[b].transform.position += 3 * groundSpaces;
                groundTrigger.transform.position += groundSpaces;
                b++;
            }
            else
            {
                b = 0;
                groundsToSpawn[b].transform.position += 3 * groundSpaces;
                groundTrigger.transform.position += groundSpaces;
                b++;
            }
        }
    }

    private void CalculateBlockade()
    {

    }

    private void CalculateOverpass()
    {

    }
}