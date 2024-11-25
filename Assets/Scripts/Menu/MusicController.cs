using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
  public static AudioManager Instance;

  

   private void Awake()
{
    DontDestroyOnLoad(gameObject);

    if (FindObjectsOfType<AudioManager>().Length > 1)
    {
        Destroy(gameObject);
    }
}

}
