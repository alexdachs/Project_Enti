using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            gameCamera.transform.position = new Vector3(
                player.transform.position.x +7,
                gameCamera.transform.position.y,
                gameCamera.transform.position.z);
        }

    }
}
