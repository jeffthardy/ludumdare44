using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneController : MonoBehaviour
{
    public int MaxTombstoneCount = 10;
    public GameObject[] TombstoneModels;

    private int nextTombstoneID;
    private string key;
    // Start is called before the first frame update
    void Start()
    {
        bool foundKey = true;
        int tombstoneID=0;
        nextTombstoneID = 0;

        //PlayerPrefs.DeleteAll();

        while (foundKey && (tombstoneID < MaxTombstoneCount))
        {
            key = "tombstone" + tombstoneID + "x";
            if (PlayerPrefs.HasKey(key))
            {
                float x = PlayerPrefs.GetFloat(key);
                key = "tombstone" + tombstoneID + "y";
                float y = PlayerPrefs.GetFloat(key);
                key = "tombstone" + tombstoneID + "z";
                float z = PlayerPrefs.GetFloat(key);
                //Debug.Log("Loading : "+ x + ", " + y + ", " + z);
                //FIXME loading all y locations to 0 so it sits even with ground
                TombstoneModels[tombstoneID].transform.localPosition = new Vector3(x, 0, z);
                tombstoneID += 1;
            }
            else
            {
                nextTombstoneID = tombstoneID;
                foundKey = false;
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void storeTombstone(Vector3 location)
    {
        Debug.Log("Saving: " + location.x + ", " + location.y + ", " + location.z);

        //Record the tombstone cordinates
        key = "tombstone" + nextTombstoneID + "x";
        PlayerPrefs.SetFloat(key, location.x);
        key = "tombstone" + nextTombstoneID + "y";
        PlayerPrefs.SetFloat(key, location.y);
        key = "tombstone" + nextTombstoneID + "z";
        PlayerPrefs.SetFloat(key, location.z);

        //Set next tombstone save id
        if(nextTombstoneID < MaxTombstoneCount -1)
            nextTombstoneID += 1;
        else
            nextTombstoneID = 0;
    }
}
