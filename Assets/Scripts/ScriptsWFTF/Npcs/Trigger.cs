using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public bool doITeleport;
    public bool doITriggerQuest;
    public bool doITriggerSpeakingQuest;
    public bool doICompleteAQuest;
    public bool doIAddJournalEntry;
    public bool doIDestroyMyself=true;

    public GameObject npc;
    public Vector3 position;
    public string npcName;
    public string questName;
    public UnityEvent onTriggering;

    public string drawingName;
    [TextArea(5, 15)]
    public string journalText;
    public void teleportNPC(GameObject npc, Vector3 position)
    {

        npc.transform.position = position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(doITeleport)
        {
            teleportNPC(npc, position);
        }
        if (doITriggerSpeakingQuest)
        {
            QuestManager.Instance.QuestToSpeak(questName,npcName);
        }
        if (doITriggerQuest)
        {
            QuestManager.Instance.makeQuestAvailable(questName, npcName);
        }
        if (doICompleteAQuest)
        {
            QuestManager.Instance.finishQuest(questName, npcName);
        }
        if (doIAddJournalEntry)
        {
            //JournalManager.Instance.AddEntry(journalText, drawingName);
        }
        onTriggering.Invoke();
        if(doIDestroyMyself)
        {
            Destroy(gameObject);
        }

    }
}
