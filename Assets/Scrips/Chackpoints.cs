using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chackpoints : MonoBehaviour
{
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("character"))
        {
            gm.lastCheckPoint = transform.position;
        }
    }
}
