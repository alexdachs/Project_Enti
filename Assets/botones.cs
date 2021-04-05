using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    private GameMaster gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    public void menu () {
        SceneManager.LoadScene("Menu");
    }
    public void recargartuto () {
        SceneManager.LoadScene("Tutorial");
    }    
    public void siguientenivel () {
        SceneManager.LoadScene("Level_1");
    }
    //Controles menu
    public void play () {
        SceneManager.LoadScene("Levels");
    }
    public void options () {
        SceneManager.LoadScene("Options");
    }
    public void credits () {
        SceneManager.LoadScene("Credits");
    }
    public void quit () {
        Application.Quit();
    }
    //Selector niveles
    public void lvl1 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Tutorial");
    }
    public void lvl2 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_1");
    }
    public void lvl3 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_2");
    }
    public void lvl4 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_3");
    }
    
}
