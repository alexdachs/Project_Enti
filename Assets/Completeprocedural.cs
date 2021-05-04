using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Completeprocedural : MonoBehaviour
{

    float timerhighscore;
    float secondshg;
    float minuteshg;
    float hourshg;
    //Level complete procedural
    public GameObject levelcompletpd;
    public GameObject textnewhighscore;
    public Text timehighscore;
    public Text timefinal;

    //Stopwatch
    float timer;
    float seconds;
    float minutes;
    float hours;

    [SerializeField] Text stopWatchText;

    //Guardados
    string savehighscore;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        timerhighscore = PlayerPrefs.GetFloat(savehighscore, 0);
    }

    // Update is called once per frame
    void Update()
    {
        stopWatchCalcul();
    }

    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.tag == "trap") {
            levelcompletpd.SetActive(true);
            Time.timeScale = 0f;
            timefinal.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            secondshg = (int)(timerhighscore % 60);
            minuteshg = (int)((timerhighscore / 60) % 60);
            hourshg = (int)(timerhighscore / 3600);
            timehighscore.text = hourshg.ToString("00") + ":" + minuteshg.ToString("00") + ":" + secondshg.ToString("00");
           
           if(timerhighscore < timer) {
            textnewhighscore.SetActive(true);
            PlayerPrefs.SetFloat(savehighscore, timer);
            }
            

        }
    }
    private void stopWatchCalcul() {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        minutes = (int)((timer / 60) % 60);
        hours = (int)(timer / 3600);
        stopWatchText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

    }
}
