using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool matchlit = false;
    public Light tlight;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !matchlit && MatchesScriptUI.match_total > 0)
        {
            matchlit = true;
            tlight.range = 20;
            tlight.enabled = true;
            MatchesScriptUI.match_total -= 1;
            StartCoroutine(FireFade()); //should wait for 10 secs
        }

    }
    IEnumerator FireFade()
    {
        for (int i=0; i < 10; i ++)
        {
            tlight.range -= 2;
            yield return new WaitForSeconds(1f);
        }
       // light.enabled = false;
        matchlit = false;
    }
}
