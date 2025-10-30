using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Windows;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    //sutvarkyti dar cia
    public Sprite noteImageChosen;
    [TextArea(5, 15)]
    public string writtenText;

    private bool isOpen = false;


    public void ShowNote()
    {
        NoteManager.Instance.noteText.text = writtenText;
        NoteManager.Instance.noteImage.overrideSprite = noteImageChosen;
        NoteManager.Instance.noteCanvas.SetActive(true);
        isOpen = true;
        HudManager.instance.DisableMovement();
        HudManager.instance.DisableMouseLook();
        HudManager.instance.DisableInteraction();
        HudManager.instance.DisableCrosshairAndInteractionText();
        HudManager.instance.noteIsActive = true;
        Debug.Log("Showing note");
    }

    void DisableNote()
    {
        NoteManager.Instance.noteCanvas.SetActive(false);
        isOpen = false;
        HudManager.instance.EnableMovement();
        HudManager.instance.EnableMouseLook();
        HudManager.instance.EnableInteraction();
        HudManager.instance.EnableCrosshairAndInteractionText();
        HudManager.instance.noteIsActive = false;
    }
    private void Update()
    { 
        if(isOpen)
        {
            if (UnityEngine.Input.anyKeyDown)
            {
                DisableNote();
            }
        }
    }

}
