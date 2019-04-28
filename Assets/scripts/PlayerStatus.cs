using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float initialMoney;
    public float invulerableTime = 3.0f;

    private float money;
    private float score;
    private bool invulerable = false;
    public bool playerInShip = false;

    private float timeWorked = 0;
    private int gamesPlayed;

    // Start is called before the first frame update
    void Start()
    {
        money = initialMoney;
        gamesPlayed = 0;
        score = 0;
    }

    // General money modifiers such as movement, time, etc use this 
    public void modifyMoney(float change)
    {
        money += change;

        // Dont allow random negatives when dying
        if (money < 0)
            money = 0;
    }

    // Track time spent at job locations earning money
    public void logWorkTime(float time)
    {
        Debug.Log("We just worked " + time + " Seconds!");
        timeWorked += time;
    }

    // Might be needed for stats
    public float getWorkTime()
    {
        return timeWorked;
    }

    // Enemies can steal money, but leave you invulnerable for a bit afterwards
    public void inflictDamage(float damage)
    {
        if (!invulerable)
        {
            modifyMoney(-damage);
            invulerable = true;
            Invoke("ClearInvulerable", invulerableTime);
        }
    }

    // Timed call to this function will reallow damage
    private void ClearInvulerable()
    {
        invulerable = false;
    }

    // Just in case something needs to know how much money we have
    public float getMoneyLevel()
    {
        return money;
    }

    // Just in case something needs to know how much money we have
    public float getScore()
    {
        return score;
    }
    

    // Track time spent at job locations earning money
    public void logBet()
    {
        gamesPlayed += 1;
    }
    // Track time spent at job locations earning money
    public int getBetCount()
    {
        return gamesPlayed;
    }

    // Update is called once per frame
    void Update()
    {
        score = money + (1000000 / Time.timeSinceLevelLoad);
    }


}
