using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static bool matchlit = false;
    public Light tlight;
    public AudioClip matchlightsound;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !matchlit && MatchesScriptUI.match_total > 0)
        {
            matchlit = true;
            AudioSource.PlayClipAtPoint(matchlightsound,transform.position);
            tlight.range = 20;
            tlight.enabled = true;
            MatchesScriptUI.match_total -= 1;
            StartCoroutine(FireFade()); //should wait for 10 secs
        }

    }
    IEnumerator FireFade()
    {
        float timer = 0;
        while (timer < 10)
        {
            timer += Time.deltaTime;
            tlight.range = 20 - (20 * (timer / 10));
            yield return new WaitForEndOfFrame();
        }
        // light.enabled = false;
        matchlit = false;
    }
}
