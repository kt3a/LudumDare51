using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
  public ParticleSystem ShootParticles;
  public CharacterAnimation CharacterAnimation;
  public LayerMask NotShootableLayer;
  public GameObject Gun;
  public GameObject Bat;
  public Light GunLight;
  public AudioSource GunShotSource;
  public AudioSource EmptyShotSource;
  public AudioSource ReloadSource;

  public float AlertRadius = 20;
  public int MagazineSize = 7;
  public float ReloadTime = 4;
  public float FireDelay = 0.6f;

  public static int CurrentAmmo = 7;
  public static int CurrentMagazines = 2;

  public static bool Reloading = false;
  private bool _swingingMelee = false;
  private float _fireDelayTimer = 0;

    void Start()
  {
    //Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Confined;
    GunLight.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {
    _fireDelayTimer -= Time.deltaTime;
    if (Input.GetButtonDown("Fire1") && CurrentAmmo > 0 && !Reloading && !_swingingMelee && _fireDelayTimer < 0 && !Movement.Running)
    {
      Shoot();
    } else if (Input.GetButtonDown("Fire1") && CurrentAmmo <= 0 && !Reloading && !_swingingMelee)
    {
      EmptyShotSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
      EmptyShotSource.Play();
    }

    if (Input.GetKeyDown(KeyCode.R) && !Reloading && CurrentMagazines > 0 && !_swingingMelee && !Movement.Running)
    {
      Reloading = true;
      CurrentMagazines--;
      StartCoroutine(Reload());
      CharacterAnimation.Reload();
      ReloadSource.Play();
    }

    if (Input.GetKeyDown(KeyCode.E) && !Reloading && !_swingingMelee && !Movement.Running)
    {
      CharacterAnimation.SwingMelee();
      _swingingMelee = true;
      Gun.SetActive(false);
      Bat.SetActive(true);
      StartCoroutine(SwingMelee());
    }
  }

  private IEnumerator SwingMelee()
  {
    yield return new WaitForSecondsRealtime(0.8f);
    _swingingMelee = false;
    Gun.SetActive(true);
    Bat.SetActive(false);
  }

  private void Shoot()
  {
    _fireDelayTimer = FireDelay;
    ShootParticles.Play();
    CharacterAnimation.Shoot();
    StartCoroutine(MuzzleFlash());
    GunShotSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
    GunShotSource.Play();
    Vector3 shootDir = ShootParticles.transform.forward;
    if (Physics.Raycast(ShootParticles.transform.position - shootDir, shootDir, out RaycastHit hit, 200, ~NotShootableLayer, QueryTriggerInteraction.Collide))
    {
      if (hit.collider.CompareTag("Zombie"))
      {
        hit.collider.GetComponent<ZombieAI>().HitZombie();
      }
    }

    CurrentAmmo--;

    foreach(ZombieAI zomb in ZombieAI.AllZombies)
    {
      if (Vector3.Distance(transform.position, zomb.transform.position) < AlertRadius)
      {
        zomb.Alert();
      }
    }
  }

  private IEnumerator Reload()
  {
    yield return new WaitForSeconds(ReloadTime);

    CurrentAmmo = MagazineSize;

    Reloading = false;
  }

  private IEnumerator MuzzleFlash()
  {
    GunLight.enabled = true;
    yield return new WaitForSeconds(0.05f);
    GunLight.enabled = false;
  }
}
