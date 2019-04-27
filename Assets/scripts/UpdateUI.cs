using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject gameFinish;
    public GameObject moneyText;
    public GameObject playerObject;
    public GameObject shipObject;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.GetComponent<Text>().text = "$" + playerObject.GetComponent<PlayerStatus>().getMoneyLevel().ToString("0.00");
        gameFinish.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.GetComponent<Text>().text = "$" + playerObject.GetComponent<PlayerStatus>().getMoneyLevel().ToString("0.00"); 

        // Handle game loss situation (no money)
        if(playerObject.GetComponent<PlayerStatus>().getMoneyLevel() == 0)
        {
            // Game Over!
            gameFinish.GetComponent<Text>().text = "GAME OVER";
        }

        //Handle ship leaving
        if (shipObject.GetComponent<ShipController>().shipLeaving == true)
        {
            //If player is on ship, we win!
            if(playerObject.GetComponent<PlayerStatus>().playerInShip == true)
            {
                gameFinish.GetComponent<Text>().text = "YOU ESCAPED!";
            }
            else
            {
                gameFinish.GetComponent<Text>().text = "THE SHIP LEFT!?";
            }
        }
    }
}
