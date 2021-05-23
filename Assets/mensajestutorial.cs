using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mensajestutorial : MonoBehaviour
{
    //mensajes tutorial
    public GameObject tutojump;
    public GameObject tutodash;
    public GameObject tutoslide;
    public GameObject tutokill;
    public GameObject tutomovement;
    public GameObject tutogravity;
    public GameObject tutogravity1;
    public GameObject tutodashreset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutojump.SetActive(false);
            tutomovement.SetActive(false);
            Time.timeScale = 1f;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tutomovement.SetActive(false);
            Time.timeScale = 1f;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tutodash.SetActive(false);
            tutokill.SetActive(false);
            tutodashreset.SetActive(false);
            Time.timeScale = 1f;

        }  
        if (Input.GetKeyDown(KeyCode.W))
        {
            tutogravity.SetActive(false);
            Time.timeScale = 1f;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tutoslide.SetActive(false);
            tutogravity1.SetActive(false);
            Time.timeScale = 1f;

        }      
    }
     private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "tutomovement") {
            tutomovement.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutojump") {
            tutojump.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutodash") {
            tutodash.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutokill") {
            tutokill.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutoslide") {
            tutoslide.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutogravity") {
            tutogravity.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutogravity1") {
            tutogravity1.SetActive(true);
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutodashreset") {
            tutodashreset.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
