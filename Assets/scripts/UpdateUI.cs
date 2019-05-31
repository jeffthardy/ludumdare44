using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    public float reloadGameTime = 5.0f;
    public GameObject gameFinish;
    public GameObject moneyText;
    public GameObject playerObject;
    public GameObject shipObject;
    public GameObject shipDistanceText;
    public GameObject workHoursText;
    public GameObject gamblingCountText;
    public GameObject tombstoneController;
    public GameObject highscoreController;
    public GameObject highscoreText;
    public GameObject scoreText;



    private bool saveTombstone = false;

    private float distanceToShip;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.GetComponent<Text>().text = "$" + playerObject.GetComponent<PlayerStatus>().getMoneyLevel().ToString("0.00");
        gameFinish.GetComponent<Text>().text = "";
        distanceToShip = Vector3.Distance(playerObject.transform.position, shipObject.transform.position);
        shipDistanceText.GetComponent<Text>().text = distanceToShip.ToString("0.0");
        workHoursText.GetComponent<Text>().text = "0";
        gamblingCountText.GetComponent<Text>().text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.GetComponent<Text>().text = "$" + playerObject.GetComponent<PlayerStatus>().getMoneyLevel().ToString("0.00"); 

        // Handle game loss situation (no money)
        if(playerObject.GetComponent<PlayerStatus>().getMoneyLevel() == 0)
        {
            playerObject.GetComponent<PlayerController>().triggerDeath();
            // Game Over!
            gameFinish.GetComponent<Text>().text = "GAME OVER";
            saveTombstone = true;
            Invoke("loadNextLevel", reloadGameTime);
        }

        //Handle ship leaving
        if (shipObject.GetComponent<ShipController>().shipLeaving == true)
        {
            //If player is on ship, we win!
            if(playerObject.GetComponent<PlayerStatus>().playerInShip == true)
            {
                playerObject.GetComponent<PlayerController>().triggerEscape();
                gameFinish.GetComponent<Text>().text = "YOU ESCAPED!";
                highscoreController.GetComponent<HighscoreController>().storeHighscore(playerObject.GetComponent<PlayerStatus>().getScore());
                Invoke("loadNextLevel", reloadGameTime);
            }
            else
            {
                gameFinish.GetComponent<Text>().text = "THE SHIP LEFT!?";
                saveTombstone = true;
                Invoke("loadNextLevel", reloadGameTime);
            }
        }

        // Update distance to ship from player
        distanceToShip = Vector3.Distance(playerObject.transform.position, shipObject.transform.position);
        shipDistanceText.GetComponent<Text>().text = distanceToShip.ToString("0.0");

        // Keep track of hours worked
        workHoursText.GetComponent<Text>().text = playerObject.GetComponent<PlayerStatus>().getWorkTime().ToString("0.0");

        // Keep track of times you have bet
        gamblingCountText.GetComponent<Text>().text = playerObject.GetComponent<PlayerStatus>().getBetCount().ToString("0");

        // Keep track of times you have bet
        highscoreText.GetComponent<Text>().text = highscoreController.GetComponent<HighscoreController>().highscore.ToString("0");

        // Keep track of times you have bet
        scoreText.GetComponent<Text>().text = playerObject.GetComponent<PlayerStatus>().getScore().ToString("0");

    }


    void loadNextLevel()
    {
        if(saveTombstone)
            tombstoneController.GetComponent<TombstoneController>().storeTombstone(playerObject.transform.position);

        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene("Menu");
    }
}
