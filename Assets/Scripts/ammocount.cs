using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ammocount : MonoBehaviour
{
    public TextMeshProUGUI textmesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shooting.CurrentAmmo == 7)
            textmesh.text = "";
        if(Shooting.CurrentAmmo == 6)
            textmesh.text = "x";
        if (Shooting.CurrentAmmo == 5)
            textmesh.text = "x  x";
        if (Shooting.CurrentAmmo == 4)
            textmesh.text = "x  x  x";
        if (Shooting.CurrentAmmo == 3)
            textmesh.text = "x  x  x  x";
        if (Shooting.CurrentAmmo == 2)
            textmesh.text = "x  x  x  x  x";
        if (Shooting.CurrentAmmo == 1)
            textmesh.text = "x  x  x  x  x x";
        if (Shooting.CurrentAmmo == 0)
            textmesh.text = "x  x  x  x  x x   x";
    }
}
