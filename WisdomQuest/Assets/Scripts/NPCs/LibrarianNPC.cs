using UnityEngine;

public class LibrarianNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueSequence initialDialogue;
    [SerializeField] private Quest[] availableQuests;
    
    private bool hasInteracted;
    
    public void Interact(PlayerController player)
    {
        if (!hasInteracted)
        {
            DialogueManager.Instance.StartDialogue(initialDialogue, OnFirstDialogueComplete);
        }
        else
        {
            ShowAvailableQuests();
        }
    }
    
    public string GetInteractionPrompt()
    {
        return hasInteracted ? "Talk to Librarian" : "Meet the Librarian";
    }
    
    private void OnFirstDialogueComplete()
    {
        hasInteracted = true;
        ShowAvailableQuests();
    }
    
    private void ShowAvailableQuests()
    {
        foreach (var quest in availableQuests)
        {
            if (!QuestManager.Instance.IsQuestActive(quest) && 
                !QuestManager.Instance.IsQuestCompleted(quest))
            {
                UIManager.Instance.ShowQuest(quest.title, quest.description);
                break;
            }
        }
    }
}