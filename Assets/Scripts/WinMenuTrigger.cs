using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenuTrigger : MonoBehaviour {
  public GameObject WinMenu;
  private ZombieAI zomb;

  private void Start()
  {
    zomb = GetComponent<ZombieAI>();
    WinMenu = GameObject.FindGameObjectWithTag("WinMenu");
  }

  private void Update()
  {
    WinMenu.SetActive(zomb.Health <= 0);
    if (zomb.Health <= 0)
    {
      Time.timeScale = 0;
    }
  }
}
