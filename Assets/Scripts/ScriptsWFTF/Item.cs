using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public ItemType type;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    public string description;
    public string itemName;
    public string functionality;
    public float restoreValue;
    public float reduceValue;
}

public enum ItemType
{
    Consumable,
    Readable,
    Objective
}