using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public GameObject model;
    public AudioClip jumpAudioData;
    AudioSource jumpAudio;

    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        jumpAudio = this.GetComponent<AudioSource>();
        jumpAudio.clip = jumpAudioData;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Money Collision!");
        if (!used)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerStatus>().modifyMoney(100);
                used = true;
                jumpAudio.Play(0);
                //Destroy object after picked up
                model.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //Destroy(transform.gameObject);            
            }
        }
    }
    
}
