using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchesScriptUI : MonoBehaviour
{
    public TextMeshProUGUI textmesh;
    public static int match_total;

    void Update()
    {
        textmesh.text = "Matches: " + match_total;
    }
}
