using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;

    public Camera playerCamera;

    private float originalFOV;


    private void Start()
    {
        originalFOV = playerCamera.fieldOfView;
    }
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E)&& currentInteractable!=null)
        {
            currentInteractable.Interact();
        }

    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(ray, out hit, playerReach))
        { 
            if(hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if(newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        HudManager.instance.EnableInteractionText(currentInteractable.interactableName, currentInteractable.interaction);
       
    }

    void DisableCurrentInteractable()
    {
        HudManager.instance.DisableInteractionText();
        if(currentInteractable)
        {
            currentInteractable = null;
        }
    }

}
