using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
  public ParticleSystem ShootParticles;
  public CharacterAnimation CharacterAnimation;

  void Start()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Confined;
  }

  // Update is called once per frame
  void Update()
  {


    if (Input.GetButtonDown("Fire1"))
    {
      ShootParticles.Play();
      CharacterAnimation.Shoot();

      Vector3 shootDir = ShootParticles.transform.forward;

      if (Physics.Raycast(ShootParticles.transform.position, shootDir, out RaycastHit hit, 200))
      {
        Debug.Log(hit.collider.name);
        if (hit.collider.CompareTag("Zombie"))
        {
          hit.collider.GetComponent<ZombieAI>().HitZombie();
        }
      }

    }
  }
}
