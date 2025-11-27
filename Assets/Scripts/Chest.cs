using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   public Player player;
   public GameObject keyUI;

    [SerializeField] private AudioClip openCh;
    [SerializeField] private AudioClip lockedChest;
    public void openChest()
   {
       if(player.hasKey)
       {
            SoundFXManager.instance.PlaySoundFXClip(openCh, transform, 0.2f);
            player.ActivateGun(); 

            keyUI.SetActive(false);
            gameObject.tag = "Untagged";
       }
       else
        {
            SoundFXManager.instance.PlaySoundFXClip(lockedChest, transform, 0.2f);
        }
   }
}
