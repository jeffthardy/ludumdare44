using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatRandomizer : MonoBehaviour
{
    public float maxHat = 1000;
    public float minHat = 1;

    private float hatSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        hatSize = Random.Range(minHat, maxHat);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(1, hatSize, 1);

    }
}
