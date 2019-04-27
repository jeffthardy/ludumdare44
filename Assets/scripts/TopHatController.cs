using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHatController : MonoBehaviour
{
    public GameObject player;
    public float hatBaseScale = 1000;
    private float hatSize = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float currentMoney = player.gameObject.GetComponent<PlayerStatus>().getMoneyLevel();
        this.transform.localScale = new Vector3(1, currentMoney/hatBaseScale, 1);
    }
}
