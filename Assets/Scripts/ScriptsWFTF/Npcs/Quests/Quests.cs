using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quests : MonoBehaviour
{
    public List<Quest> QuestList = new List<Quest>();
    public string npcName;

    private void Start()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddQuest(npcName, QuestList);
        }
    }

}
[System.Serializable]
public class Quest
{
    public string questName;
    [TextArea(5, 15)]
    public string questDescription;
    public string drawingStartQuest;
    [TextArea(5, 15)]
    public string questFinishedDesc;
    public string drawingEndQuest;

    public List<ItemsDialogue> requiredItems;
    public List<ItemsDialogue> rewards;
    public UnityEvent onQuestCompletion;
    public void onQuestComplete()
    {
        onQuestCompletion.Invoke();
    }
    public List<DialogueLine> QuestStartDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> QuestInProgressDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> QuestInCompleteDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> QuestNotAvailableDialogueLines = new List<DialogueLine>();

    public QuestState state;
    public bool isCompleted;
}

public enum QuestState
{
    notAvailable,
    notStarted,
    inProgress,
    ReadyToTurnIn
}

