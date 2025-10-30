using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager: MonoBehaviour
{
    public static NoteManager Instance { get; set; }

    public TextMeshProUGUI noteText;
    public Image noteImage;
    public GameObject noteCanvas;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
