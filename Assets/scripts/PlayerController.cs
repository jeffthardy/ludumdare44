using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject playerModel;

    public bool alive = true;
    public bool canMove = true;
    public float speed;
    public float horizontalMoveRate;
    public float upMoveRate;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    private bool isGrounded;
    private Rigidbody rb;

    // Colliders we might be able to use
    private bool shipIsAvailable;
    private Collider shipCollision;
    private bool jobIsAvailable;
    private Collider jobCollision;
    private bool gameIsAvailable;
    private Collider gameCollision;


    // Stat tracking for using locations
    private bool  isWorking;
    public AudioClip jumpAudioData;
    AudioSource jumpAudio;
    //private float jobTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isWorking = false;
        jumpAudio = this.GetComponent<AudioSource>();
        jumpAudio.clip = jumpAudioData;

    }

    void FixedUpdate()
    {
        
        if (alive && canMove)
        {
            // Handle normal sideways movement
            float moveHorizontal = Input.GetAxis("Horizontal") * horizontalMoveRate;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            rb.AddForce(movement * speed);


            // Handle Jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpAudio.Play(0);
            }
                                          

            // Attempt to use areas that are behind character
            float moveVertical = Input.GetAxis("Vertical");
            if (moveVertical > 0)
            {
                //Debug.Log("Attempting to use Object!");
                if (shipIsAvailable)
                {
                    shipCollision.gameObject.GetComponent<ShipController>().useShip();
                    if (shipCollision.gameObject.GetComponent<ShipController>().shipLeaving == true)
                    {
                        //We can leave!
                        canMove = false;
                        this.GetComponent<PlayerStatus>().playerInShip = true;
                        //Debug.Log("WE entered ship and used it!");
                        playerModel.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        //Ship is ignoring us
                    }
                }
                if (jobIsAvailable)
                {
                    if (!isWorking)
                    {
                        //jobTime = 0;
                    }
                    else
                        this.gameObject.GetComponent<PlayerStatus>().logWorkTime(Time.deltaTime);
                    //jobTime += Time.deltaTime;
                    isWorking = true;
                    jobCollision.gameObject.GetComponent<JobController>().UseJob();
                }
                if (gameIsAvailable)
                {
                    gameCollision.gameObject.GetComponent<GamblingController>().UseGame();
                    this.gameObject.GetComponent<PlayerStatus>().logBet();
                }
            }
            else
            {
                //If we stopped working we should record our time worked
                if (isWorking)
                {
                    isWorking = false;
                    //jobTime += Time.deltaTime;
                    this.gameObject.GetComponent<PlayerStatus>().logWorkTime(Time.deltaTime);
                }
            }
        }

    }

    void OnCollisionStay()
    {
        if (!isGrounded && rb.velocity.y <= 0)
            isGrounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerController TriggerEnter! " + other.gameObject.tag);

        if (other.gameObject.tag == "Ship")
        {
            //Debug.Log("PlayerController detected ship!");
            shipIsAvailable = true;
            shipCollision = other;
        }

        if (other.gameObject.tag == "Job")
        {
            //Debug.Log("PlayerController detected job!");
            jobIsAvailable = true;
            jobCollision = other;
        }

        if (other.gameObject.tag == "Gambling")
        {
            //Debug.Log("PlayerController detected gambling!");
            gameIsAvailable = true;
            gameCollision = other;
        }


        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("PlayerController detected Enemy!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("PlayerController TriggerExit! " + other.gameObject.tag);
        if (other.gameObject.tag == "Ship")
        {
            //Debug.Log("PlayerController detected leaving ship!");
            shipIsAvailable = false;
            shipCollision = null;
        }
        if (other.gameObject.tag == "Job")
        {
            //Debug.Log("PlayerController detected leaving job!");
            jobIsAvailable = false;
            jobCollision = null;
        }
        if (other.gameObject.tag == "Gambling")
        {
            //Debug.Log("PlayerController detected leaving Gambling!");
            gameIsAvailable = false;
            gameCollision = null;
        }
    }

}