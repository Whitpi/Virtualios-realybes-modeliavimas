using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 0.1f;
    public float ammo = 6;
    public Animator gunAnimator;

    public float targetsHit = 0;

    public Player player;

    public ObjectiveManager objManager;

    public Camera camera;

    public TextMeshProUGUI bulletCounter;

    public GameObject bulletBoxUI;
    public GameObject muzzleFlash;

    private float nextTimeToFire = 0f;

    [SerializeField] private AudioClip shootLoaded;
    [SerializeField] private AudioClip shootEmpty;
    [SerializeField] private AudioClip reload;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            if (ammo > 0)
                {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                ammo -= 1;
                bulletCounter.text = "x " + ammo;
                SoundFXManager.instance.PlaySoundFXClip(shootLoaded, transform, 0.2f);
                muzzleFlash.SetActive(true);
                Invoke("disableMuzzleFlash", 0.1f);
            }
            else
            {
                SoundFXManager.instance.PlaySoundFXClip(shootEmpty, transform, 0.2f);
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && player.hasAmmo)
        {
            ammo = ammo + 6;
            player.hasAmmo = false;
            bulletBoxUI.SetActive(false);
            bulletCounter.text = "x " + ammo;
            SoundFXManager.instance.PlaySoundFXClip(reload, transform, 0.2f);
        }
    }

    void Shoot()
    {
        gunAnimator.SetTrigger("shoot");
        RaycastHit hit;

        
        
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
                targetsHit += 1;
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    void disableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}
