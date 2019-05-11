using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float hitCost = 100.0f;

    //Linear scale volume based on distance


    private bool isGrounded;
    private Rigidbody rb;
    private int directive;
    private float moveHorizontal = 0;

    public AudioClip StaticAudioData;
    public AudioClip HitAudioData;
    AudioSource Audio;
    private GameObject listener;
    public float volumeDistance;
    public float maxVolume=1.0f;
    private float listenerDistance;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Invoke("SetDirective", 2);
        directive = 0;

        Audio = this.GetComponent<AudioSource>();
        Audio.clip = StaticAudioData;

        // Find player, which has audio listener attached to child camera
        listener = FindObjectOfType<PlayerController>().gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float volume;
        listenerDistance = Vector3.Distance(listener.transform.position, gameObject.transform.position);
        if (listenerDistance > volumeDistance)
            volume = 0;
        else
        {
            volume = maxVolume * ((volumeDistance - listenerDistance) / volumeDistance);
            //Debug.Log("listenerDistance: " + listenerDistance);
            //Debug.Log("volume: " + volume);
        }
        Audio.volume = volume;

        switch (directive)
        {
            case 0: // Hang out
                if (moveHorizontal != 0)
                    Audio.Pause();
                moveHorizontal = 0.0f;
                break;
            case 1: // move left
                if ((moveHorizontal == 0) || (!Audio.isPlaying))
                {
                    Audio.clip = StaticAudioData;
                    Audio.loop = true;
                    Audio.Play(0);
                }
                moveHorizontal = -20.0f;
                break;
            case 2: // move right
                if ((moveHorizontal == 0) || (!Audio.isPlaying))
                {
                    Audio.clip = StaticAudioData;
                    Audio.loop = true;
                    Audio.Play(0);
                }
                moveHorizontal = 20.0f;
                break;
            default:  //unsure
                break;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //Debug.Log("moving " + movement);
        rb.AddForce(movement * speed);
    }


    //Particle controller
    public ParticleSystem collisionParticles;
    public float maxParticleLifetime = 0.7f;

    void HandleParticleEvents()
    {
        collisionParticles.transform.position = this.transform.position;
        if (collisionParticles.isStopped)
            collisionParticles.Play();
    }


    private void SetDirective()
    {
        directive = (int)Random.Range(1, 3);
        Invoke("SetDirective", Random.Range(1.0f, 3.0f));
        //Debug.Log("Changing direction to " + directive);

    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyController TriggerEnter!");

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("EnemyController detected player!");
            other.gameObject.GetComponent<PlayerStatus>().inflictDamage(hitCost);
            Audio.Pause();
            Audio.loop = false;
            Audio.clip = HitAudioData;
            Audio.Play(0);
            HandleParticleEvents();
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
