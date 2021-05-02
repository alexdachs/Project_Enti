using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public float speed = 75.0f;
    private float waitTime;
    public float startWaitTime = 1.0f;

    public bool canShoot = false;
    public float timeBtwShots;
    public float startBtwTimeShots = 5.0f;
    public float shootDistance = 100.0f;



    public Transform[] moveSpots;
    private int randomSpot;

    private Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character").transform;
        timeBtwShots = 0f;

        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
      /*  transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpots[randomSpot].position.x, this.transform.position.y), speed * Time.deltaTime);
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
        }*/

        if (timeBtwShots <= 0 && canShoot)
        {
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y -15f, transform.position.z), Quaternion.identity);
            timeBtwShots = startBtwTimeShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "activator")
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "activator")
        {
            canShoot = false;
        }
    }
}
