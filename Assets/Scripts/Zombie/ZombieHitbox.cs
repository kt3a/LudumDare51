using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitbox : MonoBehaviour
{
  private bool _touchingPlayer;
  private bool _hitting = false;
  private bool _readyToStartHitting = true;

  public void EnableHit()
  {
    if (_readyToStartHitting) { 
      _hitting = true;
      _readyToStartHitting = false;
    }
  }

  public void DisableHit()
  {
    _hitting = false;
    _readyToStartHitting = true;
  }

  private void Update()
  {
    if (_touchingPlayer && _hitting)
    {
      Debug.Log("Bro just got punched");
      _hitting = false;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      _touchingPlayer = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      _touchingPlayer = false;
    }
  }
}
