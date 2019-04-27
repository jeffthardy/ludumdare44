using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float initialMoney;
    public float invulerableTime = 3.0f;

    private float money;
    private bool invulerable = false;
    public bool playerInShip = false;

    // Start is called before the first frame update
    void Start()
    {
        money = initialMoney;        
    }

    public void modifyMoney(float change)
    {
        if((invulerable == true) && (change < 0))
        {
            //ignore

        }
        else
            money += change;

        // Dont allow random negatives when dying
        if (money < 0)
            money = 0;
    }

    public void inflictDamage()
    {
        invulerable = true;
        Invoke("ClearInvulerable", invulerableTime);
    }

    private void ClearInvulerable()
    {
        invulerable = false;
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
