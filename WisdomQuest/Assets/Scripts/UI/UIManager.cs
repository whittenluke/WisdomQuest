using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [Header("Player HUD")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Slider experienceBar;
    
    [Header("Interaction UI")]
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TextMeshProUGUI interactionText;
    
    [Header("Quest UI")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;
    
    private PlayerData playerData;
    
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
    
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        UpdateHUD();
    }
    
    public void UpdateHUD()
    {
        if (playerData == null) return;
        
        levelText.text = $"Level {playerData.playerLevel}";
        experienceText.text = $"{playerData.experience:F0} / {playerData.playerLevel * 100f:F0}";
        experienceBar.value = playerData.experience / (playerData.playerLevel * 100f);
    }
    
    public void ShowInteractionPrompt(string prompt)
    {
        interactionPrompt.SetActive(true);
        interactionText.text = prompt;
    }
    
    public void HideInteractionPrompt()
    {
        interactionPrompt.SetActive(false);
    }
    
    public void ShowQuest(Quest quest)
    {
        questPanel.SetActive(true);
        questTitle.text = quest.title;
        questDescription.text = quest.description;
    }
    
    public void HideQuest()
    {
        questPanel.SetActive(false);
    }
}