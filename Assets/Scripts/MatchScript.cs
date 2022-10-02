using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScript : MonoBehaviour
{
    int match_amt = 3;
    public AudioClip collection;
    public GameObject player;

    void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(collection, player.transform.position);
        MatchesScriptUI.match_total += match_amt;
    }
}
