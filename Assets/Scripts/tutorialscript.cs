using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialscript : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = true;
        }
    }

}
