using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour {
  private List<ZombieAI> _touchedZombies;

  private bool _hitting = false;
  private bool _readyToStartHitting = true;

  public void EnableHit()
  {
    if (_readyToStartHitting)
    {
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
    if (_touchedZombies.Count > 0 && _hitting)
    {
      foreach(ZombieAI zomb in _touchedZombies)
      {
        zomb.HitZombie(34);
      }
      _hitting = false;
    }
  }

  void Start()
  {
    _touchedZombies = new List<ZombieAI>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<ZombieAI>())
    {
      _touchedZombies.Add(other.GetComponent<ZombieAI>());
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.GetComponent<ZombieAI>())
    {
      _touchedZombies.Remove(other.GetComponent<ZombieAI>());
    }
  }
}
