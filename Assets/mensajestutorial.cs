using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mensajestutorial : MonoBehaviour
{
    //mensajes tutorial
    public GameObject tutojump;
    bool tutojumpbool;
    public GameObject tutodash;
    bool tutodashbool;
    public GameObject tutoslide;
    bool tutoslidebool;
    public GameObject tutokill;
    bool tutokillbool;
    public GameObject tutomovement;
    bool tutomovementbool;
    public GameObject tutogravity;
    bool tutogravitybool;
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            tutodash.SetActive(false);
            tutokill.SetActive(false);
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
            Time.timeScale = 1f;

        }      
    }
     private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "tutomovement") {
            tutomovement.SetActive(true);
            tutomovementbool = true;
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutojump") {
            tutojump.SetActive(true);
            tutojumpbool = true;
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutodash") {
            tutodash.SetActive(true);
            tutodashbool = true;
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutokill") {
            tutokill.SetActive(true);
            tutokillbool = true;
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutoslide") {
            tutoslide.SetActive(true);
            tutoslidebool = true;
            Time.timeScale = 0f;
        }
        if(col.gameObject.tag == "tutogravity") {
            tutogravity.SetActive(true);
            tutogravitybool = true;
            Time.timeScale = 0f;
        }
    }
}
