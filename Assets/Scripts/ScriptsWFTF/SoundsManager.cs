using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; set; }
    public AudioClip[] glassShatter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

   public void setActiveMusic(GameObject music)
   {
        music.SetActive(true);
   }
    public void setInactiveMusic(GameObject music)
    {
        music.SetActive(false);
    }

    public void playSoundClip(AudioClip clip)
    {
        SoundFXManager.instance.PlaySoundFXClip(clip, transform, 0.5f);
    }


}
