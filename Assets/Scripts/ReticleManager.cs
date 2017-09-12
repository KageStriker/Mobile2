using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReticleManager : GameManager
{
    private float index;

    private float amplitudeX = 1f;
    private float amplitudeY = 1f;
    private float omegaX = 0.8f;
    private float omegaY = 0.8f;

    private GameObject[] reticles;
    
    private void Start()
    {

    }

    private void Update()
    {
        if (enemies != null && player)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i].activeSelf)
                {

                }
            }
        }
    }
}