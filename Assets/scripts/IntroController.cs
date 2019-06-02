using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(IntroPlayer());

    }

    IEnumerator IntroPlayer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level1");
    }
}
