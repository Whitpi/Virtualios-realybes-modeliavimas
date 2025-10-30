using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool hasKey = false;
    public bool hasAmmo = false;

    public GameObject gunObject;
    public GameObject shovel;

    public GameObject keyUI;
    public GameObject ammoUI;
    public GameObject bulletUI;

    public void giveKey()
    {
        hasKey = true;
        keyUI.SetActive(true);
    }

    public void giveAmmo()
    {
        hasAmmo = true;
        ammoUI.SetActive(true);
    }

    public void ActivateGun()
    {
        gunObject.SetActive(true);
        shovel.SetActive(false);
        bulletUI.SetActive(true);
    }
}
