using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance => instance;
    
    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    
    [SerializeField] private Quest[] allQuests; // Reference to all available quests
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AcceptQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            quest.OnQuestAccepted();
            SaveManager.Instance.SaveGame();
        }
    }
    
    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.OnQuestCompleted();
            SaveManager.Instance.SaveGame();
        }
    }
    
    public bool IsQuestActive(Quest quest) => activeQuests.Contains(quest);
    public bool IsQuestCompleted(Quest quest) => completedQuests.Contains(quest);
    
    public string[] GetActiveQuestIds()
    {
        return activeQuests.Select(q => q.name).ToArray();
    }
    
    public string[] GetCompletedQuestIds()
    {
        return completedQuests.Select(q => q.name).ToArray();
    }
    
    public void LoadSaveData(QuestSaveData data)
    {
        activeQuests.Clear();
        completedQuests.Clear();
        
        foreach (string questId in data.activeQuestIds)
        {
            var quest = allQuests.FirstOrDefault(q => q.name == questId);
            if (quest != null)
            {
                activeQuests.Add(quest);
            }
        }
        
        foreach (string questId in data.completedQuestIds)
        {
            var quest = allQuests.FirstOrDefault(q => q.name == questId);
            if (quest != null)
            {
                completedQuests.Add(quest);
            }
        }
    }
}