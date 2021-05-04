using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    public GameMaster gm;
    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    public void menu () {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
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
        Time.timeScale = 1f;
    }
    public void lvl2 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1f;
    }
    public void lvl3 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_2");
        Time.timeScale = 1f;
    }
    public void lvl4 () {
        gm.lastCheckPoint = new Vector3(-270, -143, 0);
        SceneManager.LoadScene("Level_3");
        Time.timeScale = 1f;
    }
    public void infinitlvl() {
        gm.lastCheckPoint = new Vector3(0, 24, 0);
        SceneManager.LoadScene("Procedural");
        Time.timeScale = 1f;
    }

}
