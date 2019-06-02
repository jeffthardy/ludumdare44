using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject playerModel;

    public bool alive = true;
    public bool canMove = true;
    public float speed;
    public float maxSpeed;
    public float horizontalMoveRate;
    public float upMoveRate;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public int extraJumps=1;

    private bool isGrounded;
    private Rigidbody rb;
    private int extraJumpCount;
    private bool jumpHeldDown;

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
    public AudioClip workAudioData;
    public AudioClip DeathAudioData;
    public AudioClip EscapeAudioData;
    public AudioClip gameAudioData;
    AudioSource Audio;
    //private float jobTime;
    private bool playedFinish = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isWorking = false;
        Audio = this.GetComponent<AudioSource>();
        Audio.clip = jumpAudioData;
        extraJumpCount = 0;

    }

    private float horizontalInput;
    private float verticalInput;
    private float pendingJumps=0;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && !jumpHeldDown)
        {
            jumpHeldDown = true;
            pendingJumps++;
        }
        else
        {
            jumpHeldDown = false;
        }
    }

        void FixedUpdate()
    {

        if (alive && canMove)
        {

            // Handle normal sideways movement
            float moveHorizontal = horizontalInput * horizontalMoveRate;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            rb.AddForce(movement * speed);

            // Handle Jump Input
            if(pendingJumps>0)
            {
                pendingJumps=0;
                if (isGrounded)
                {
                    extraJumpCount = extraJumps;
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                    isGrounded = false;

                    Audio.Pause();
                    Audio.clip = jumpAudioData;
                    Audio.Play(0);

                }
                else
                {
                    if (extraJumpCount > 0)
                    {
                        extraJumpCount--;
                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                        isGrounded = false;

                        Audio.Pause();
                        Audio.clip = jumpAudioData;
                        Audio.Play(0);

                    }
                }
            }

            // Fix max horzSpeed, which also comes into play when jumping
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

            // Attempt to use areas that are behind character
            float moveVertical = verticalInput;
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
                    if (!Audio.isPlaying)
                    {
                        Audio.clip = workAudioData;
                        Audio.Play(0);
                    }

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
                    if (!Audio.isPlaying)
                    {
                        Audio.clip = gameAudioData;
                        Audio.Play(0);
                    }

                    gameCollision.gameObject.GetComponent<GamblingController>().UseGame();
                    this.gameObject.GetComponent<PlayerStatus>().logBet();
                }
            }
            else
            {
                if (isGrounded)
                {
                    // WE aren't using something or jumping, so kill any audio
                    Audio.Pause();
                }
                //If we stopped working we should record our time worked
                if (isWorking)
                {
                    isWorking = false;
                    //jobTime += Time.deltaTime;
                    this.gameObject.GetComponent<PlayerStatus>().logWorkTime(Time.deltaTime);
                }
            }
        }

        // Display movement particles as needed
        handleParticleDisplay();

    }

    //Particle controller
    public ParticleSystem moveParticles;
    public float maxParticleLifetime = 0.7f;

    void handleParticleDisplay()
    {
        if (isGrounded)
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1)
            {
                if (moveParticles.isStopped)
                    moveParticles.Play();

                var main = moveParticles.main;
                main.startLifetime = maxParticleLifetime * (Mathf.Abs(rb.velocity.x) / maxSpeed);


                Vector3 rotation = moveParticles.transform.eulerAngles;
                if (rb.velocity.x < 0)
                {
                    Vector3 target = new Vector3(-45, 90, rotation.z);
                    if (rotation != target)
                        moveParticles.transform.eulerAngles = target;
                }
                else
                {
                    Vector3 target = new Vector3(-45, -90, rotation.z);
                    if (rotation != target)
                        moveParticles.transform.eulerAngles = target;

                }
            }
            else
            {
                var main = moveParticles.main;
                main.startLifetime = 0;
                if (moveParticles.isPlaying)
                    moveParticles.Stop();
            }
        } else
        {
            // Handle particles when in the air?
            if (moveParticles.isStopped)
                moveParticles.Play();

            var main = moveParticles.main;
            main.startLifetime = maxParticleLifetime * (Mathf.Abs(rb.velocity.y) / maxSpeed);

            // Just drop particles down any time we are in the air... 
            Vector3 rotation = moveParticles.transform.eulerAngles; 
            Vector3 target = new Vector3(90, 0, rotation.z);
            if (rotation != target)
                moveParticles.transform.eulerAngles = target; 

        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (!isGrounded && (rb.velocity.y <= 0) && (collisionInfo.collider.gameObject.tag == "Ground"))
            isGrounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("PlayerController TriggerEnter! " + other.gameObject.tag);

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

    public void triggerDeath()
    {
        if (!Audio.isPlaying && !playedFinish)
        {
            Audio.clip = DeathAudioData;
            Audio.Play(0);
            playedFinish = true;
        }

    }
    public void triggerEscape()
    {
        if (!Audio.isPlaying && !playedFinish)
        {
            Audio.clip = EscapeAudioData;
            Audio.Play(0);
            playedFinish = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("PlayerController TriggerExit! " + other.gameObject.tag);
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