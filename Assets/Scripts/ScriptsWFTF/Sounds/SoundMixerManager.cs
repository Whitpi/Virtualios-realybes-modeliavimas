using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider musicSlider;
    public float mVolume;
    public float fxVolume;
    public float musicVolume;

    //Uzkraunami garso nustatymai
    private void Start()
    {
        LoadVolumeSettings();
    }
    //Nusatomas pagrindinis garsas, tuo paciu ir issaugojamas.
    public void setMasterVolume(float level)
    {
        PlayerPrefs.SetFloat("MasterVolume", level);
        PlayerPrefs.Save();
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20);
    }

    //Nusatomi garso efektu garsai, tuo paciu ir issaugojami.
    public void setSoundFXVolume(float level)
    {
        PlayerPrefs.SetFloat("SoundFXVolume", level);
        PlayerPrefs.Save();
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20);
    }
    //Nustatomas muzikos garsas, ir issaugojamas
    public void setMusicVolume(float level)
    {
        PlayerPrefs.SetFloat("MusicVolume", level);
        PlayerPrefs.Save();
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20);
    }
    //Garso uzkrovimo metodas
    private void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        float fxVolume = PlayerPrefs.GetFloat("SoundFXVolume", 0.5f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        masterSlider.value = masterVolume;
        fxSlider.value = fxVolume;
        musicSlider.value = musicVolume;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(fxVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }



}
