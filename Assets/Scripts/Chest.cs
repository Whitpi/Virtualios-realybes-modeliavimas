using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
   public Player player;
   public GameObject keyUI;
   public void openChest()
   {
       if(player.hasKey)
       {
            player.ActivateGun(); 

            keyUI.SetActive(false);
       }
   }
}
