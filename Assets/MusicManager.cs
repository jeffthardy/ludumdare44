using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (instance != null && (instance != this))
        {
            // Destroy the master singleton if going to a scene we don't use the music player
            if (((scene.name != "Menu") && (scene.name != "About")))
                Destroy(instance.gameObject);

            Destroy(this.gameObject);

            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // any other methods you need
}

