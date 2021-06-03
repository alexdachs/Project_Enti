using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
    public character player;
    private AudioSource coin;

    // Start is called before the first frame update
    void Start()
    {
        coin = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.coin == true)
        {
            coin.Play();
        }
    }
}
