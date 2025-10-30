using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; set; }
    public AudioClip[] gunksDestroy;
    public AudioClip lobotomyMinigame;

    public AudioClip[] whispers;

    public AudioClip[] ominousOutsideMusic;
    public AudioClip[] ominousInsideMusic;

    public AudioClip[] outsideAmbience;
    public AudioClip[] insideAmbience;

    public AudioClip LeechSpawn;
    public AudioClip LeechDeath;

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
