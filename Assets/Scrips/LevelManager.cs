using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentX = 4000;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load("5", typeof(GameObject)), new Vector3 (0,0,0), Quaternion.identity);
        //InvokeRepeating("instanciaPrefab", 1.0f, 1.0f);
    }

    public void instanciaPrefab(string number)
    {
        GameObject trozo = Instantiate(Resources.Load(number, typeof(GameObject))) as GameObject;

        trozo.transform.position = new Vector3(currentX, 0, 0);
        currentX += 4000;
    }

    void instanciaNext()
    {

    }
}
