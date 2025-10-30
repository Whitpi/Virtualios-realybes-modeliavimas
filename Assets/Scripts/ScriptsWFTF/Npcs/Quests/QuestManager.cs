using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

     private Dictionary<string, List<Quest>> npcQuests = new Dictionary<string, List<Quest>>();
    public static QuestManager Instance { get; set; } //Singleton, visada yra scenoje


    //Sukuriame jei nera secenoje
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
    public void AddQuest(string npcName, List<Quest> quests)
    {
        if (!npcQuests.ContainsKey(npcName))
        {
            npcQuests[npcName] = quests;
        }
        
    }

    public Quest GetCurrentQuest(string npcName)
    {
        if (!npcQuests.ContainsKey(npcName))
        {
            Debug.LogWarning($"sis NPC neturi dabar aktyvaus quest'o!");
            return null;
        }

        Quest firstNotAvailableQuest = null;

        foreach (var quest in npcQuests[npcName])
        {
            if (!quest.isCompleted)
            {
                if (quest.state != QuestState.notAvailable)
                {
                    return quest;
                }
                else if (firstNotAvailableQuest == null)
                {
                    firstNotAvailableQuest = quest;
                }
            }
        }

        if (firstNotAvailableQuest != null)
        {
            return firstNotAvailableQuest;
        }

        Debug.Log($"Visi NPC quest'ai yra uþbaigti.");
        return null;
    }
    // Uzbaigti quest'a
    public void CompleteQuest(string npcName)
    {
        Quest quest = GetCurrentQuest(npcName);
        if (quest != null)
        {
            //// pasalinam quest itemus is inventoriaus
            //foreach (var item in quest.requiredItems)
            //{
            //    InventoryManager.Instance.RemoveItem(item.item, item.quantity);
            //}

            //// Duoti rewards
            //foreach (var reward in quest.rewards)
            //{
            //   for(int i = 0; i<reward.quantity;i++)
            //   {
            //        InventoryManager.Instance.AddItem(reward.item);
            //   }

            //}
            quest.onQuestComplete();
            quest.isCompleted = true;

            Debug.Log($"Quest {quest.questName} uzbaigtas");
        }
    }

    public void makeQuestAvailable(string questName, string npcName)
    {
        foreach (var quest in npcQuests[npcName])
        {
            if (quest.questName == questName)
            {
                quest.state = QuestState.notStarted;
            }

        }
    }

    public void QuestToSpeak(string questName, string npcName)
    {
        foreach (var quest in npcQuests[npcName])
        {
            if (quest.questName == questName)
            {
                quest.state = QuestState.ReadyToTurnIn;
            }

        }
    }
    public void finishQuest(string questName, string npcName)
    {
        foreach (var quest in npcQuests[npcName])
        {
            if (quest.questName == questName)
            {
                quest.isCompleted = true;
            }

        }
    }

}
