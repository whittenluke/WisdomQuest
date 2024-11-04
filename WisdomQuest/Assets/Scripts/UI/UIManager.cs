using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;
    
    [Header("Interaction UI")]
    [SerializeField] private GameObject interactionPromptPanel;
    [SerializeField] private TextMeshProUGUI interactionPromptText;
    
    [Header("Player HUD")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;
    
    [Header("Quest UI")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questDescriptionText;
    
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
    
    public void ShowInteractionPrompt(string prompt)
    {
        interactionPromptPanel.SetActive(true);
        interactionPromptText.text = prompt;
    }
    
    public void HideInteractionPrompt()
    {
        interactionPromptPanel.SetActive(false);
    }
    
    public void UpdateHUD(int level, float experience)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
        if (experienceText != null)
            experienceText.text = $"XP: {experience:F0}";
    }
    
    public void ShowQuest(Quest quest)
    {
        if (questPanel != null)
        {
            questPanel.SetActive(true);
            questTitleText.text = quest.title;
            questDescriptionText.text = quest.description;
        }
    }
    
    public void HideQuest()
    {
        if (questPanel != null)
        {
            questPanel.SetActive(false);
        }
    }
}