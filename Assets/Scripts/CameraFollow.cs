using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject skybox;

    private void Update()
    {
        if (GameManager.Instance.player != null)
        {
            transform.position = new Vector3(GameManager.Instance.player.position.x + 10, transform.position.y, transform.position.z); 
        }
        skybox.transform.Rotate(new Vector3(0, 0, -0.1f));
    }
}
