using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{

    public GameObject ObjectToGenerate;
    public float GenerationSpeed;
    public int MaxObjects;

    private int objectCount;
    private GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting to Generate objects");
        objectCount = 0;

        ObjectToGenerate.SetActive(false);

        StartCoroutine(GenerationController());
        
    }

    IEnumerator GenerationController()
    {
        while (objectCount < MaxObjects)
        {
            Vector3 pos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            pos += transform.position;
             

            clone = Instantiate(ObjectToGenerate, pos, transform.rotation);
            clone.SetActive(true);
            Debug.Log("Generating object #" + objectCount);
            objectCount++;
            yield return new WaitForSeconds(GenerationSpeed);
        }
    }
    
}
