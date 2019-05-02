using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float moneyDecayRate = (float)-1.0;
    public bool enableDebug = false;
    public GameObject player;
    public float moveThreshold;
    private Rigidbody playerRB;
    private PlayerStatus myPlayerStatus;


    public AudioClip StartingAudioData;
    AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        Invoke("playerMoneyDrainer", 1.0f);

        Audio = this.GetComponent<AudioSource>();
        Audio.clip = StartingAudioData;
        Audio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(enableDebug)
            if((playerRB.velocity.x != 0) || (playerRB.velocity.y != 0) || (playerRB.velocity.z != 0))
                Debug.Log("Velocity:" +playerRB.velocity);

        updateMoney();
        updatePlayerStatus();
    }

    void updateMoney()
    {
        // Reduce money if moving faster than minimum rate
        if ((Math.Abs(playerRB.velocity.x) >= moveThreshold) || (Math.Abs(playerRB.velocity.y) >= moveThreshold) || (Math.Abs(playerRB.velocity.z) >= moveThreshold))
        {
            double velocity_mag = Math.Abs(playerRB.velocity.magnitude);
            player.GetComponent<PlayerStatus>().modifyMoney(moneyDecayRate* (float)velocity_mag);
            if(enableDebug)
                Debug.Log("Money:" + player.GetComponent<PlayerStatus>().getMoneyLevel());
        }

        // Apply other money modifiers
    }

    void updatePlayerStatus()
    {
        // Detect when player runs out of cash and kill player
        if (player.GetComponent<PlayerStatus>().getMoneyLevel() <= 0)
            player.GetComponent<PlayerController>().alive = false;

    }

    void playerMoneyDrainer()
    {
        player.GetComponent<PlayerStatus>().modifyMoney(-1);
        Invoke("playerMoneyDrainer", 0.5f);
    }
}
