using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerParts : MonoBehaviour
{
    GameObject player;
    public AudioClip collection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false); //hide
            AudioSource.PlayClipAtPoint(collection, player.transform.position); //collect it
            // add to the total amt to be displayed in the UI
            FlameThrowerUI.total_parts += 1;
        }
    }
}