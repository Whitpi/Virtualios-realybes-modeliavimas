using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string interactableName;
    public string interaction;
    public UnityEvent onInteraction;
    public void Interact()
    {
        onInteraction.Invoke();
    }

    //public void changeName(string newName)
    //{
    //    newName = name;
    //}

}
