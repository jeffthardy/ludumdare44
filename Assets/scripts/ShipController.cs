using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    private bool available = false;
    public bool shipLeaving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useShip()
    {
        if (available)
        {
            // Player leaves world and wins?
            shipLeaving = true;
        }
        else
        {
            // Player can't use this right now... 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ship Boarding Entered!");

        if (other.gameObject.tag == "Player")
        {
            available = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Ship Boarding Exited!");

        if (other.gameObject.tag == "Player")
        {
            available = false;
        }
    }
}
