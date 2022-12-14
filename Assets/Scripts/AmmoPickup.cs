using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
  public AudioClip PickupSound;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Shooting.CurrentMagazines += 1;
      AudioSource.PlayClipAtPoint(PickupSound, transform.position);
      Destroy(gameObject);
    }
  }
}
