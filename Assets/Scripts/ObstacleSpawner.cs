using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private List<GameObject> obstaclesToSpawn;

    private static int obstacleNum;

    private void Start()
    {
        obstaclesToSpawn = new List<GameObject>();
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obstaclesToSpawn.Add(g);
        }

        for (int i = 0; i < obstaclesToSpawn.Count; i++)
        {
            obstaclesToSpawn[obstacleNum].transform.position = new Vector3(obstacleNum * 80, 0, -5);
            obstaclesToSpawn[obstacleNum].SetActive(true);
            obstacleNum++;
        }
    }

    private void Update()
    {
        
    }
}
