using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Money Collision!");

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatus>().modifyMoney(100);
            //Destroy object after picked up
            Destroy(transform.gameObject);            
        }
    }
    
}
