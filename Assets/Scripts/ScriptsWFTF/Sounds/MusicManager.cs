using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource musicSource;

    private AudioClip[] music;
    private AudioClip[] ambience;
    public static MusicManager Instance { get; set; }
    public float intervalBetweenSongs = 10f;
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
    private void Start()
    {
        music = SoundsManager.Instance.ominousOutsideMusic;
        ambience = SoundsManager.Instance.outsideAmbience;
        Invoke("StartPlayingSfx", 3f);
    }

    public void StartPlaying()
    {
        StartCoroutine(PlayMusicAtIntervals());
    }

    public void StartPlayingSfx()
    {
        StartCoroutine(playAmbientSounds());
    }

    private IEnumerator PlayMusicAtIntervals()
    {
        while (true)
        {
            Debug.Log("Playing music");
            AudioClip clip = music[Random.Range(0, music.Length)];
            musicSource.clip = clip;
            musicSource.Play();
            yield return new WaitForSeconds(clip.length + intervalBetweenSongs);
        }
    }
    private IEnumerator playAmbientSounds()
    {
        while (true)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(ambience, transform, 0.1f);
            yield return new WaitForSeconds(Random.Range(30f, 60f));
        }
    }

    public void changeToInsideMusic()
    {
        StopAllCoroutines();
        music = SoundsManager.Instance.ominousInsideMusic;
        ambience = SoundsManager.Instance.insideAmbience;
        StartPlaying();
        StartPlayingSfx();
    }

    public void changeToOutsideMusic()
    {
        StopAllCoroutines();
        music = SoundsManager.Instance.ominousOutsideMusic;
        ambience = SoundsManager.Instance.outsideAmbience;
        StartPlaying();
        StartPlayingSfx();
    }
}
