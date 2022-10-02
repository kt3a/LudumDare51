using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ammocount : MonoBehaviour {
  public List<GameObject> BulletIcons;
  public List<GameObject> MagIcons;
  public GameObject BulletEmptyText;
  public GameObject MagEmptyText;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    //Turn off everthing
    foreach (var go in BulletIcons)
    {
      go.SetActive(false);
    }

    foreach (var go in MagIcons)
    {
      go.SetActive(false);
    }

    BulletEmptyText.SetActive(false);
    MagEmptyText.SetActive(false);



    //Turn back on the stuff we want
    if (Shooting.CurrentAmmo > 0)
    {
      for (int i = 0; i < Shooting.CurrentAmmo; i++)
      {
        BulletIcons[i].SetActive(true);
      }
    }
    else
    {
      BulletEmptyText.SetActive(true);
    }

    //Turn back on the stuff we want
    if (Shooting.CurrentMagazines > 0)
    {
      for (int i = 0; i < Mathf.Min(Shooting.CurrentMagazines, MagIcons.Count); i++)
      {
        MagIcons[i].SetActive(true);
      }
    }
    else
    {
      MagEmptyText.SetActive(true);
    }
  }
}
