using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScript : MonoBehaviour
{
    public static int totalhealth = 100;
    public GameObject InnerBar;
    private CharacterAnimation Animation;
    private InputHandler InputHandler;
    private Flamethrower Flamethrower;
    private Shooting Shooting;

    private Image healthbar;
    //public GameObject player = GameObject.FindGameObjectWithTag("Player");

    void Start()
    {
        healthbar = InnerBar.GetComponent<Image>();
        Animation = FindObjectOfType<CharacterAnimation>();
        InputHandler = FindObjectOfType<InputHandler>();
        Flamethrower = FindObjectOfType<Flamethrower>(true);
        Shooting = FindObjectOfType<Shooting>();
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
            healthbar.fillAmount = totalhealth / 100f;
            Animation.Die();
            Shooting.enabled = false;
            InputHandler.enabled = false;
            Flamethrower.enabled = false;
        }

    }
}
