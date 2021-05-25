using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClearManager : MonoBehaviour
{
    [SerializeField] Animator levelClearAnimator;
    [SerializeField] Text playerTimeText;
    [SerializeField] Text minTimeText;
    [SerializeField] GameObject collectableText;
    [SerializeField] GameObject collectableTextNo;
    public void ShowLevelClear (int stars, float playertime, float mintime, bool collectable) {
            int secondsf = (int)(playertime % 60);
            int minutesf = (int)((playertime / 60) % 60);
            int hoursf = (int)(playertime / 3600);
            playerTimeText.text = hoursf.ToString("00 ") + ":" + minutesf.ToString(" 00 ") + ":" + secondsf.ToString(" 00 ");
            secondsf = (int)(mintime % 60);
            minutesf = (int)((mintime / 60) % 60);
            hoursf = (int)(mintime / 3600);
            minTimeText.text = hoursf.ToString("00 ") + ":" + minutesf.ToString(" 00 ") + ":" + secondsf.ToString(" 00 ");
            if (collectable) {
                collectableText.SetActive(true);
                collectableTextNo.SetActive(false);
            }
            levelClearAnimator.SetInteger("stars", stars);
    }
}
