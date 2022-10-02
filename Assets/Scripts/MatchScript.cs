using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScript : MonoBehaviour
{
    int match_amt = 3;
 
    void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
        MatchesScriptUI.match_total += match_amt;
    }
}
