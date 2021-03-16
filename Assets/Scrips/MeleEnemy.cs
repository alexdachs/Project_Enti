using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{

    public float speed = 1.0f;
    private float waitTime;
    public float startWaitTime = 1.0f;

    public Transform[] moveSpots;
    private int randomSpot;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpots[randomSpot].position.x, this.transform.position.y), speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
