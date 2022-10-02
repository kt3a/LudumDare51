using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlameThrowerUI : MonoBehaviour
{
    public static int total_parts = 0;
    public TextMeshProUGUI textmesh;

    // Update is called once per frame
    void Update()
    {
        if (total_parts < 4)
        {
            textmesh.text = "Flame Thrower Parts: " + total_parts + " of 4";
        }
        if (total_parts == 4)
        {
            textmesh.text = "You have the Flame Thrower.\nGo Find Big Mama.\nDefeat Big Mama";
        }
    }
}
