using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public float speed = 75.0f;
    private float waitTime;
    public float startWaitTime = 1.0f;

    public float timeBtwShots;
    public float startBtwRimeShots = 2.0f;
    public float shootDistance = 15.0f;


    public Transform[] moveSpots;
    private int randomSpot;

    private Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character").transform;
        timeBtwShots = startBtwRimeShots;

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

        if (timeBtwShots <= 0 && Vector2.Distance(transform.position, player.position) < shootDistance)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startBtwRimeShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
