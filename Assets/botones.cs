using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    public void menu () {
        SceneManager.LoadScene("Menu");
    }
    public void recargartuto () {
        SceneManager.LoadScene("Tutorial");
    }    
    public void siguientenivel () {
        SceneManager.LoadScene("Level_1");
    }
}
