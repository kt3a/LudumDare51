using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSFX : MonoBehaviour {
  public List<AudioClip> AmbientClips;
  private GameObject _player;
  // Start is called before the first frame update
  void Start()
  {
    _player = GameObject.FindGameObjectWithTag("Player");
    StartCoroutine(AmbientCo());
  }

  private IEnumerator AmbientCo()
  {
    while (true)
    {
      yield return new WaitForSecondsRealtime(Random.Range(10, 40));
      Debug.Log("playing clip");
      AudioSource.PlayClipAtPoint(AmbientClips[Random.Range(0, AmbientClips.Count)], _player.transform.position + Random.onUnitSphere * 30);
    }
  }
}
