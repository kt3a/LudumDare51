using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScript : MonoBehaviour
{
    public static int totalhealth = 100;
    public GameObject InnerBar;
    private Image healthbar;
    //public GameObject player = GameObject.FindGameObjectWithTag("Player");

    void Start()
    {
        healthbar = InnerBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalhealth > 0)
        {
            healthbar.fillAmount = totalhealth / 100f;
        }
        if(totalhealth < 0)
        {
            //signal to play the death animation -- use boolean?
        }

    }
}
