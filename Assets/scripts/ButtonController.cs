using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton;

    // Start is called before the first frame update
    void Start()
    {
        m_YourFirstButton.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        //Output this to console when Button1
        Debug.Log("You have clicked the start button!");
        SceneManager.LoadScene("Level1");

    }
}
