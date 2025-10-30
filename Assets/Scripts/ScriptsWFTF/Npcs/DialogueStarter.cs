using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    public Dialogue Dialogue;
    public string npcName; 
    public bool spokenWith;
    public bool randomLines;

    public void StartDialogue()
    {
        Quest currentQuest = QuestManager.Instance.GetCurrentQuest(npcName);

        if (currentQuest != null)
        {
            if (currentQuest.state == QuestState.inProgress && HasRequiredItems(currentQuest))
            {
                currentQuest.state = QuestState.ReadyToTurnIn;
                Debug.Log($"Quest '{currentQuest.questName}' galima jau atiduoti");
            }
            Dialogue questDialogue = GetQuestDialogue(currentQuest);
            DialogueManager.Instance.DialogueStart(questDialogue);
            if (currentQuest.state == QuestState.ReadyToTurnIn)
            {
                QuestManager.Instance.CompleteQuest(npcName);
            }
        }
        else
        {
            if (randomLines && Dialogue.defaultDialogueLines.Count > 1)
            {
                List<DialogueLine> randomLines = new List<DialogueLine>();

                int randomIndex = Random.Range(0, Dialogue.defaultDialogueLines.Count);

                randomLines.Add(Dialogue.defaultDialogueLines[randomIndex]);

                Dialogue randomDialogue = new Dialogue();

                randomDialogue.defaultDialogueLines = randomLines;

                DialogueManager.Instance.DialogueStart(randomDialogue);
                return;
            }

            DialogueManager.Instance.DialogueStart(Dialogue);
        }
    }

    private Dialogue GetQuestDialogue(Quest quest)
    {
        Dialogue dialogue = new Dialogue();
        if (quest.state == QuestState.notStarted)
        {
            dialogue.defaultDialogueLines = quest.QuestStartDialogueLines;
            quest.state = QuestState.inProgress; // Kai pradejom questa, po pirmo pasnekejimo perdarom i progresuota quest'a
            //JournalManager.Instance.AddEntry(quest.questDescription,quest.drawingStartQuest);
        }
        else if (quest.state == QuestState.inProgress)
        {
            dialogue.defaultDialogueLines = quest.QuestInProgressDialogueLines;
        }
        else if (quest.state == QuestState.ReadyToTurnIn)
        {
            dialogue.defaultDialogueLines = quest.QuestInCompleteDialogueLines;
            //JournalManager.Instance.AddEntry(quest.questFinishedDesc,quest.drawingEndQuest);
        }
        else if (quest.state == QuestState.notAvailable)
        {
            if(quest.QuestNotAvailableDialogueLines.Count == 0)
            {
                dialogue.defaultDialogueLines = Dialogue.defaultDialogueLines;
            }
            else
            dialogue.defaultDialogueLines = quest.QuestNotAvailableDialogueLines;
        }


        return dialogue;
    }

    private bool HasRequiredItems(Quest quest)
    {
        foreach (var item in quest.requiredItems)
        {
            if (item.item == null)
            {
                return true;
            }
            //if (!InventoryManager.Instance.HasItem(item.item, item.quantity))
            //{
            //    Debug.Log("quest itemu neturi zaidejas");
            //    return false;
            //}
        }
        return true;
    }
}

[System.Serializable]
public class ItemsDialogue
{
    public Item item;
    public int quantity;
}

[System.Serializable]
public class DialogueLine
{
    public NpcDialogueInfo character;
    [TextArea(5, 15)] //Kad lengviau butu ivesti teksta
    public string text; //Dialogo tekstas
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> defaultDialogueLines = new List<DialogueLine>(); //Sukuriame dialogo sarasa
    public UnityEvent afterDialogueEvent;
}
