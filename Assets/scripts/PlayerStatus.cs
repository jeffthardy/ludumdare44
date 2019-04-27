using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float initialMoney;
    private float money;
    public bool playerInShip = false;

    // Start is called before the first frame update
    void Start()
    {
        money = initialMoney;        
    }

    public void modifyMoney(float change)
    {
        money += change;
        // Dont allow random negatives when dying
        if (money < 0)
            money = 0;
    }

    public float getMoneyLevel()
    {
        return money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
