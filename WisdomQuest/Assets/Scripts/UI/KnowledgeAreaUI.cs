using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KnowledgeAreaUI : MonoBehaviour, IInteractable
{
    [SerializeField] private string areaName;
    [SerializeField] private TextMeshProUGUI areaNameText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressBar;
    
    private PlayerData playerData;
    
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        areaNameText.text = areaName;
        UpdateProgress();
    }
    
    private void UpdateProgress()
    {
        if (playerData.knowledgeAreas.TryGetValue(areaName, out float progress))
        {
            progressText.text = $"{progress:F1}%";
            progressBar.value = progress / 100f;
        }
    }
    
    public void Interact(PlayerController player)
    {
        // Show area details and available knowledge pieces
        var pieces = KnowledgeManager.Instance.GetKnowledgePieces(areaName);
        if (pieces.Count > 0)
        {
            // Update to provide both title and description
            UIManager.Instance.ShowQuest($"Study {areaName}", $"Explore knowledge pieces in the {areaName} area.");
        }
    }
    
    public string GetInteractionPrompt()
    {
        return $"Study {areaName}";
    }
}