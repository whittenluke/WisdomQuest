using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questDescriptionText;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private GameObject questPanel;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHUD(int level, float experience)
    {
        UpdateLevelText(level);
        UpdateExperienceText((int)experience, 100);
    }

    public void ShowInteractionPrompt()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);
    }

    public void HideInteractionPrompt()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    public void ShowQuest(string title, string description)
    {
        if (questPanel != null)
        {
            questPanel.SetActive(true);
            UpdateQuestInfo(title, description);
        }
    }

    private void UpdateLevelText(int level)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }

    private void UpdateExperienceText(int currentExp, int maxExp)
    {
        if (experienceText != null)
            experienceText.text = $"XP: {currentExp}/{maxExp}";
    }

    private void UpdateQuestInfo(string title, string description)
    {
        if (questTitleText != null)
            questTitleText.text = title;
        if (questDescriptionText != null)
            questDescriptionText.text = description;
    }
}