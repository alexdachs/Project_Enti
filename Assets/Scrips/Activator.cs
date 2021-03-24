using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D activator;

    // Start is called before the first frame update
    void Start()
    {
        activator = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            activator.transform.position = new Vector3(
                player.transform.position.x + 200,
                activator.transform.position.y,
                activator.transform.position.z);
        }

    }
}
