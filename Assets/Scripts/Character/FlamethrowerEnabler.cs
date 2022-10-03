using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerEnabler : MonoBehaviour
{
  public Shooting ShootingScript;
  public GameObject Flamethrower;
  public GameObject AmmoUI;
  public GameObject MagsUI;
  public bool FlamethrowerEnabled = false;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.M))
    {
      ToggleFlamethrower();
    }
  }

  public void ToggleFlamethrower ()
  {
    FlamethrowerEnabled = !FlamethrowerEnabled;
    Flamethrower.SetActive(FlamethrowerEnabled);
    ShootingScript.enabled = !FlamethrowerEnabled;
    MagsUI.SetActive(false);
    AmmoUI.SetActive(false);
  }
}
