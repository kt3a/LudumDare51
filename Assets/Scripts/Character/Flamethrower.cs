using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {
  public GameObject Effects;

  void Update()
  {
    bool shooting = Input.GetMouseButton(0) && !Movement.Running;

    Effects.SetActive(shooting);

    if (shooting) { 
      foreach(ZombieAI zomb in ZombieAI.AllZombies)
      {
        Vector3 zombDir = zomb.transform.position - transform.position;

        if ((Vector3.Angle(transform.forward, zombDir) < 30 || zombDir.magnitude < 2) && zombDir.magnitude < 9)
        {
          zomb.HitZombie(Time.deltaTime * 150, true);
        }
      }

      foreach (BurnableFence fence in BurnableFence.Fences)
      {
        Vector3 fenceDir = fence.transform.position - transform.position;

        if ((Vector3.Angle(transform.forward, fenceDir) < 30 || fenceDir.magnitude < 2) && fenceDir.magnitude < 9)
        {
          fence.StartBurning();
        }
      }
    }
  }
}
