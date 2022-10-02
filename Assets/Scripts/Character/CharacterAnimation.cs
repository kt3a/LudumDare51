using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterAnimation : MonoBehaviour {
  public Transform ModelRoot;
  public Transform SpineBone;
  public Transform ParticleSystemHolder;
  public Animator Animator;

  private Vector3 _aimDir = Vector3.zero;
  private Vector3 _walkDir = Vector3.zero;
  private Vector3 _currentModelVector = Vector3.zero;
  private float shootAngleOffset = 0;

  private float _lerpRate = 5;

  private void LateUpdate()
  {
    if (HealthScript.totalhealth <= 0)
      return;

    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");

    float walkSpeed = Mathf.Sqrt(x*x + y * y);

    shootAngleOffset = Mathf.Lerp(shootAngleOffset, 0, Time.deltaTime * 5);
    Vector3 mousePos = Input.mousePosition;
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;

    float mouseX = mousePos.x - screenWidth / 2;
    float mouseY = mousePos.y - screenHeight / 2;

    _aimDir = Vector3.Lerp(_aimDir, new Vector3(mouseX, 0, mouseY), Time.deltaTime * _lerpRate);
    _currentModelVector = Vector3.Lerp(_currentModelVector, _aimDir, Time.deltaTime * _lerpRate);
    _walkDir = Vector3.Lerp(_walkDir, new Vector3(x, 0, y), Time.deltaTime * _lerpRate);

    float walkAngle = Vector3.SignedAngle(Vector3.forward, _walkDir, Vector3.up);

    if (walkSpeed < 0.1)
    {
      _walkDir = Vector3.Lerp(_walkDir, _aimDir, Time.deltaTime * _lerpRate);
    }

    float mouseAngle = Vector3.SignedAngle(Vector3.forward, _aimDir, Vector3.up);
    float walkAimDiff = Vector3.SignedAngle(_aimDir, _walkDir, Vector3.up);
    float modelAngle = Vector3.SignedAngle(Vector3.forward, _currentModelVector, Vector3.up);
    Animator.SetFloat("WalkAimDiff", Mathf.Abs(walkAimDiff));
    Animator.SetFloat("WalkRight", Mathf.Sign(walkAimDiff));
    Animator.SetFloat("Speed", walkSpeed);

    mouseAngle -= modelAngle;

    Vector3 modelRot = ModelRoot.eulerAngles;
    modelRot.y = modelAngle + 35;
    ModelRoot.eulerAngles = modelRot;

    Vector3 spineForward = Quaternion.Euler(0, 45, 0) * _aimDir;
    Quaternion rot = Quaternion.AngleAxis(-shootAngleOffset, SpineBone.right);


    SpineBone.forward = rot * spineForward;
    ParticleSystemHolder.forward = new Vector3(mouseX, 0, mouseY);
  }

  internal void Die()
  {
    Animator.SetBool("Dead", true);
    Animator.transform.localPosition = new Vector3(0, -0.921f, 0);
  }

  internal void SwingMelee()
  {
    Animator.SetTrigger("SwingMelee");
  }

  internal void Reload()
  {
    Animator.SetTrigger("Reload");
  }

  public void Shoot()
  {
    shootAngleOffset = 7;
  }
}
