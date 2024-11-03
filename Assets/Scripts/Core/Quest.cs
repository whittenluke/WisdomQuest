using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Quest", menuName = "WisdomQuest/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public float experienceReward;
    public KnowledgePiece[] knowledgeRewards;
    
    public UnityEvent onQuestAccepted;
    public UnityEvent onQuestCompleted;
    
    public void OnQuestAccepted()
    {
        onQuestAccepted?.Invoke();
    }
    
    public void OnQuestCompleted()
    {
        onQuestCompleted?.Invoke();
        
        // Give rewards to player
        var playerData = GameObject.FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            playerData.AddExperience(experienceReward);
            foreach (var knowledge in knowledgeRewards)
            {
                KnowledgeManager.Instance.AddKnowledgePiece(knowledge);
            }
        }
    }
}