using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    float timer;
    float seconds;
    float minutes;
    float hours;

    [SerializeField] Text stopWatchText;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        stopWatchCalcul ();
    }
    void stopWatchCalcul() {
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
        hours = timer / 3600;

        stopWatchText.text = hours + ":" + minutes + ":" + seconds;


    }
}
