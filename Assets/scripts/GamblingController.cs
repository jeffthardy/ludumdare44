using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingController : MonoBehaviour
{
    public GameObject player;

    private bool available = false;
    private bool gameUsed = false;
    public float gamePayment = 100.0f;
    public float gameCost = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseGame()
    {
        if (available)
        {
            // Player leaves world and wins?
            gameUsed = true;
            //Debug.Log("Job Used!");
            player.gameObject.GetComponent<PlayerStatus>().modifyMoney(-gameCost);

            // Decide if we win and only then pay out
            int number = (int)Random.Range(0, 101);
            if (number >= 88)
            {
                Debug.Log("You won game!");
                player.gameObject.GetComponent<PlayerStatus>().modifyMoney(gamePayment);
            }
            else
            {
                Debug.Log("You lost game!");
            }
        }
        else
        {
            // Player can't use this right now... 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Boarding Entered!");

        if (other.gameObject.tag == "Player")
        {
            available = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Game Boarding Exited!");

        if (other.gameObject.tag == "Player")
        {
            available = false;
        }
    }
}
