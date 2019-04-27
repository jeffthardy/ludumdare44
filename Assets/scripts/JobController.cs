using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : MonoBehaviour
{
    public GameObject player;

    private bool available = false;
    private bool jobUsed = false;
    public float jobPayment = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void useJob()
    {
        if (available)
        {
            // Player leaves world and wins?
            jobUsed = true;
            Debug.Log("Job Used!");
            player.gameObject.GetComponent<PlayerStatus>().modifyMoney(jobPayment);
        }
        else
        {
            // Player can't use this right now... 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Job Boarding Entered!");

        if (other.gameObject.tag == "Player")
        {
            available = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Job Boarding Exited!");

        if (other.gameObject.tag == "Player")
        {
            available = false;
        }
    }
}
