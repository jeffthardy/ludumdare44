using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_StartButton;
    public Button m_AboutButton;
    public Button m_QuitButton;
    public Button m_MenuButton;


    //#if UNITY_WEBGL	
    //     public static string webplayerQuitURL = "http://google.com";
    //#endif

    // Start is called before the first frame update
    void Start()
    {
        if(m_StartButton)
            m_StartButton.onClick.AddListener(StartOnClick);
        if(m_AboutButton)
            m_AboutButton.onClick.AddListener(AboutOnClick);
        if(m_QuitButton)
            m_QuitButton.onClick.AddListener(QuitOnClick);
        if(m_MenuButton)
            m_MenuButton.onClick.AddListener(MenuOnClick);
    }

    void MenuOnClick()
    {
        Debug.Log("You have clicked the Menu button!");
        SceneManager.LoadScene("Menu");
    }

    void StartOnClick()
    {
        Debug.Log("You have clicked the start button!");
        SceneManager.LoadScene("Level1Intro");
    }

    void AboutOnClick()
    {
        Debug.Log("You have clicked the start button!");
        SceneManager.LoadScene("About");
    }

    void QuitOnClick()
    {
        Debug.Log("You have clicked the quit button!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL	
        //Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif

    }
}
