using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void ChangeScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Test_lvl");
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Test_lvl"))
        {
            if (Input.GetKey(KeyCode.P))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Victory"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
    }
}