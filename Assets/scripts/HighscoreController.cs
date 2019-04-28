using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour
{
    public float highscore;

    private const string key = "highscore";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(key))
        {
            highscore = PlayerPrefs.GetFloat(key);
        }
        else
        {
            highscore = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void storeHighscore(float score)
    {
        if(score > highscore)
        {
            PlayerPrefs.SetFloat(key, score);
            highscore = score;
        }
    }
}
