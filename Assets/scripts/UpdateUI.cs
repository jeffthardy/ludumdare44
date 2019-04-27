using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    public float reloadGameTime = 5.0f;
    public Scene myReloadScene;
    public GameObject gameFinish;
    public GameObject moneyText;
    public GameObject playerObject;
    public GameObject shipObject;
    public GameObject shipDistanceText;

    private float distanceToShip;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.GetComponent<Text>().text = "$" + playerObject.GetComponent<PlayerStatus>().getMoneyLevel().ToString("0.00");
        gameFinish.GetComponent<Text>().text = "";
        distanceToShip = Vector3.Distance(playerObject.transform.position, shipObject.transform.position);
        shipDistanceText.GetComponent<Text>().text = distanceToShip.ToString("0.0");
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
            Invoke("reloadLevel", reloadGameTime);
        }

        //Handle ship leaving
        if (shipObject.GetComponent<ShipController>().shipLeaving == true)
        {
            //If player is on ship, we win!
            if(playerObject.GetComponent<PlayerStatus>().playerInShip == true)
            {
                gameFinish.GetComponent<Text>().text = "YOU ESCAPED!";
                Invoke("reloadLevel", reloadGameTime);
            }
            else
            {
                gameFinish.GetComponent<Text>().text = "THE SHIP LEFT!?";
                Invoke("reloadLevel", reloadGameTime);
            }
        }

        // Update distance to ship from player
        distanceToShip = Vector3.Distance(playerObject.transform.position, shipObject.transform.position);
        shipDistanceText.GetComponent<Text>().text = distanceToShip.ToString("0.0");

    }


    void reloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
