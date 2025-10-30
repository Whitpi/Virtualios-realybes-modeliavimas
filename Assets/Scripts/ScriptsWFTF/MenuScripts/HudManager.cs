using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    [Header("Kodai kurie isjungiami")]
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private PlayerInteraction interaction;
    public GameObject Crosshair;
    public bool noteIsActive = false;
    public Animator gun;
    public Animator shovel;


    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        mouseLook = FindAnyObjectByType<MouseLook>();
        interaction = FindAnyObjectByType<PlayerInteraction>();
    }
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;



    public void DisableCrosshairAndInteractionText()
    {
        interactionText.transform.parent.gameObject.SetActive(false);
    }
    public void EnableCrosshairAndInteractionText()
    {
        interactionText.transform.parent.gameObject.SetActive(true);
    }
    public void EnableInteractionText(string name, string interaction)
    {

        interactionText.text = $"<color=#FFFF00>" + name + "</color>\n\n" + interaction + " (E)";


        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText() 
    {
        interactionText.gameObject.SetActive(false);
    }

    public void DisableMovement()
    {
        playerMovement.isMoving = false;
        playerMovement.enabled = false;
        gun.SetBool("WhenMoving", false);
        shovel.SetBool("WhenMoving", false);
        //playerAnim.SetBool("IsMoving", false);
        Debug.Log("should not be moving");
    }
    public void DisableMouseLook()
    {
        mouseLook.enabled = false;
    }
    public void DisableInteraction()
    {
        interaction.enabled = false;
    }

    public void EnableMovement()
    {
        playerMovement.enabled = true;
    }
    public void EnableMouseLook()
    {
        mouseLook.enabled = true;
    }
    public void EnableInteraction()
    {
        interaction.enabled = true;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
