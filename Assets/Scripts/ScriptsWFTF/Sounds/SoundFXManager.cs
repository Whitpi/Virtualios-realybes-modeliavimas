using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    //Singletonas
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //Sukuriamas objektas scenoje su audiosource
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Objektui duodamas klipas
        audioSource.clip = clip;

        //Objektui duodamas garsas
        audioSource.volume = volume;

        //Grojamas garsas
        audioSource.Play();

        //Ilgis garso efekto
        float clipLength = audioSource.clip.length;

        //Sunaikinamas objektas po to kai baigiasi garsas
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        //Priskiriame atsitiktini indeksa
        int rand = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClipWithPitchChange(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip[rand];

        audioSource.volume = volume;

        audioSource.pitch = Random.Range(0.2f,0.7f);

        //Atsitiktinai nustatome kurioje puseje gros garsas (ausyje)
        double randPan= Random.Range(0,2);
        Debug.Log("RANDOM PUSES RNG:" + randPan);

        //Tik 1 arba -1 (desine, kaire)
        if(randPan <= 0.5)
        {
            Debug.Log("DESINE");
            audioSource.panStereo = 1;
        }
        else
        {
            Debug.Log("KAIRE");
            audioSource.panStereo = -1;
        }

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
    public void playSoundFX(AudioClip clip)
    {
        PlaySoundFXClip(clip, transform, 1f);
    }

}
