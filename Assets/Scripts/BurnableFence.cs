using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableFence : MonoBehaviour {
  public static List<BurnableFence> Fences = new List<BurnableFence>();
  public GameObject burnEffects;
  private bool _burning = false;

  private void Awake()
  {
    Fences.Clear();
  }

  void Start()
  {
    Fences.Add(this);
  }

  public void StartBurning()
  {
    if (_burning)
      return;

    burnEffects.SetActive(true);
    Destroy(gameObject, 5);
  }

  private void OnDestroy()
  {
    Fences.Remove(this);
  }
}
