using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {
  public AudioClip PickupSound;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      HealthScript.totalhealth += 25;
      if (HealthScript.totalhealth > 100)
      {
        HealthScript.totalhealth = 100;
      }
      AudioSource.PlayClipAtPoint(PickupSound, transform.position);

      Destroy(gameObject);
    }
  }
}
