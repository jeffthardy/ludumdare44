using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float fixedIntroTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroPlayer());

    }

    IEnumerator IntroPlayer()
    {
        yield return new WaitForSeconds(fixedIntroTime);
        SceneManager.LoadScene("Level1");
    }
}
