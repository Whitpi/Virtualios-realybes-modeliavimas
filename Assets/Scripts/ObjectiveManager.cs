using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;


    public void changeObjectiveText(string objective)
    {
        objectiveText.text = "Current objective: " + objective;
    }
}
