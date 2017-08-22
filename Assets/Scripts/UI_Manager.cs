using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private float amplitudeX;
    private float amplitudeY;

    private float omegaX;
    private float omegaY;

    private float index;

    protected float x;
    protected float y;

    private void Update()
    {
        
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }
}