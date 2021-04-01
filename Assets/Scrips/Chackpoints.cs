using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chackpoints : MonoBehaviour
{
    private GameMaster gm;
    private BoxCollider2D player;
    private float suelo;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("character").GetComponent<BoxCollider2D>();
        suelo = GetComponent<SpriteRenderer>().bounds.min.y + 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("character"))
        {
            /*float center_x = (player.bounds.min.x + player.bounds.max.x) / 2;
            Vector2 centerPosition = new Vector2(center_x, player.bounds.min.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up); */

            gm.lastCheckPoint = new Vector3(transform.position.x, suelo, transform.position.z);
        }
    }

    /*private Vector3 checkRaycastHit(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                return hit.transform.position;
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
    }*/
}
