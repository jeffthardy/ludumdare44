using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float fixedIntroTime = 2.0f;
    public GameObject villain;

    private AudioSource villainAudio;
    private bool introStarted;

    // Start is called before the first frame update
    void Start()
    {
        introStarted = false;
        villainAudio = villain.GetComponent<AudioSource>();
        StartCoroutine(IntroPlayer());

    }

    private void Update()
    {
        // Finished audio intro
        if((villainAudio.isPlaying == false) && (introStarted == true))
        {
            //Speech is over... start level
            SceneManager.LoadScene("Level1");
        }

    }

    IEnumerator IntroPlayer()
    {
        // Wait for camera to fade in
        yield return new WaitForSeconds(fixedIntroTime);

        //Listen to villan's speech
        villainAudio.Play();
        introStarted = true;
    }
}
