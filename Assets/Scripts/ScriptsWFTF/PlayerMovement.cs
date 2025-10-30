using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    [SerializeField] private AudioClip[] moveClips;

    public float speed = 20f;
    public float gravity = -19.81f;
    Vector3 velocity;

    public bool isMoving;
    private bool canPlaySound = true;

    MouseLook mouseLook;

    public Animator shovel;
    public Animator gun;

    public bool isShovelActive = false;
    public bool isGunActive = false;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook = GetComponentInChildren<MouseLook>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * (speed + 3) * Time.deltaTime); // Padidinam greiti begimui
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime); // Normalus greitis
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *Time.deltaTime);

        //Tikriname ar zaidejas juda
        if (move.magnitude > 0.01f)
        {
            isMoving = true;
            if(shovel.gameObject.active)
            {
                shovel.SetBool("WhenMoving", true);
            }
            else if(gun.gameObject.active)
            {
                gun.SetBool("WhenMoving", true);
            }

        }
        else
        {
            isMoving = false;
            if (shovel.gameObject.active)
            {
                shovel.SetBool("WhenMoving", false);
            }
            else if (gun.gameObject.active)
            {
                gun.SetBool("WhenMoving", false);
            }
        }


    }
    private IEnumerator PlaySoundWithDelay(float time)
    {
        canPlaySound = false; 
        SoundFXManager.instance.PlayRandomSoundFXClip(moveClips, transform, 0.2f);
        yield return new WaitForSeconds(time); 
        canPlaySound = true;
    }

}
