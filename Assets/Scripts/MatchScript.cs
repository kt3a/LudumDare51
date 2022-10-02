using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScript : MonoBehaviour
{
    int match_amt = 3;
    public AudioClip collection;
    public GameObject player;

  private void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) { 
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(collection, player.transform.position);
        MatchesScriptUI.match_total += match_amt;
      }
    }
}
