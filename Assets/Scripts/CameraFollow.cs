using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public GameObject skybox;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + 9, transform.position.y, transform.position.z);
        skybox.transform.Rotate(new Vector3(0, 0, -0.1f));
    }
}
