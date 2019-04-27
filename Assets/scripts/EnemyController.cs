using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float hitCost = 100.0f;

    private bool isGrounded;
    private Rigidbody rb;
    private int directive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Invoke("SetDirective", 2);
        directive = 0;

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = 0;
        switch (directive)
        {
            case 0: // Hang out
                moveHorizontal = 0.0f;
                break;
            case 1: // move left
                moveHorizontal = -20.0f;
                break;
            case 2: // move right
                moveHorizontal = 20.0f;
                break;
            default:  //unsure
                break;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //Debug.Log("moving " + movement);
        rb.AddForce(movement * speed);
    }

    private void SetDirective()
    {
        directive = (int)Random.Range(1, 3);
        Invoke("SetDirective", Random.Range(1.0f, 3.0f));
        //Debug.Log("Changing direction to " + directive);

    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyController TriggerEnter!");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("EnemyController detected player!");

            other.gameObject.GetComponent<PlayerStatus>().modifyMoney(-hitCost);
            other.gameObject.GetComponent<PlayerStatus>().inflictDamage();
        }

        if (directive == 1)
            directive = 2;
        else if (directive == 2)
            directive = 1;


    }

    void OnTriggerExit(Collider other)
    {

    }

}
