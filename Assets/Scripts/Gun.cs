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

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            ammo -= 1;
            bulletCounter.text = "x " + ammo;
        }

        if(Input.GetKeyDown(KeyCode.R) && player.hasAmmo)
        {
            ammo = ammo + 6;
            player.hasAmmo = false;
            bulletBoxUI.SetActive(false);
            bulletCounter.text = "x " + ammo;
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
                //if(targetsHit < 4)
                //{
                //    objManager.changeObjectiveText("bottles shot " +targetsHit + "/4");
                //}
                //else
                //{
                //    objManager.changeObjectiveText("celebrate, you did it!");
                //}


            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
