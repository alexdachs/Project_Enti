using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_sound : MonoBehaviour
{
    public character player;
    private AudioSource dashSound;

    // Start is called before the first frame update
    void Start()
    {
        dashSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAttacking == false)
        {
            dashSound.Play();
        }
    }
}
