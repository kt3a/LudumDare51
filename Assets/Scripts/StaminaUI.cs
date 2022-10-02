using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour {
  public Image healthbar;

  void Update()
  {
    healthbar.fillAmount = Movement.Stamina/100f;
  }
}
