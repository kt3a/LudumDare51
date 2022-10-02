using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour {
  
  public Light DirectionalLight;
  public Color LightningColor;
  public static bool CurrentlyLightninging;
  public List<AudioClip> ThunderSounds;

  void Start()
  {
    StartCoroutine(LightningCo());
  }

  private IEnumerator LightningCo()
  {
    while (true)
    {
      CurrentlyLightninging = false;
      yield return new WaitForSeconds(10);
      CurrentlyLightninging = true;
      StartCoroutine(PlayThunderSound());

      int startFlashes = Random.Range(2, 5);
      int endFlashes = Random.Range(2, 5);

      for (int i = 0; i < startFlashes; i++)
      {
        float duration = Random.Range(0.05f, 0.1f);
        float intensity = Random.Range(.3f, 0.6f);

        yield return StartCoroutine(Flash(duration, intensity));
      }

      yield return StartCoroutine(Flash(Random.Range(1.2f, 1.3f), 1));

      for (int i = 0; i < startFlashes; i++)
      {
        float duration = Random.Range(0.05f, 0.1f); 
        float intensity = Random.Range(.3f, 0.6f);

        yield return StartCoroutine(Flash(duration, intensity));
      }
    }
  }

  private IEnumerator PlayThunderSound()
  {
    yield return new WaitForSeconds(Random.Range(1.5f, 4f));


    AudioClip thunderClip = ThunderSounds[Random.Range(0, ThunderSounds.Count)];

    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

    AudioSource.PlayClipAtPoint(thunderClip, player.position + Random.onUnitSphere * 50, 200);
  }

  private IEnumerator Flash(float duration, float maxIntensity)
  {
    float timer = 0;

    Vector3 lightDir = DirectionalLight.transform.eulerAngles;
    lightDir.y = Random.Range(0, 360);
    DirectionalLight.transform.eulerAngles = lightDir;

    while (timer < duration)
    {
      float intentsity = Mathf.Sin(Mathf.PI * (timer/duration)) * maxIntensity;
      DirectionalLight.intensity = intentsity * intentsity;

      timer += Time.deltaTime;

      yield return new WaitForEndOfFrame();
    }

    DirectionalLight.intensity = 0;
  }
}
