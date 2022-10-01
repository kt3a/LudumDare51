using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTorsoAim : MonoBehaviour {
  public Transform spineBone;
  private float rot = 0;


  void LateUpdate()
  {
    rot += Input.GetAxis("Horizontal") * Time.deltaTime * 200;
    Vector3 euler = spineBone.rotation.eulerAngles;

    euler.y = rot;

    spineBone.rotation = Quaternion.Euler(euler);

  }
}
