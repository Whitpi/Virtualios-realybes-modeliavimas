using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/NpcInfo")]
public class NpcDialogueInfo : ScriptableObject
{
    public string npcName;
    public Color npcColor;
    public AudioClip npcVoice;
}
