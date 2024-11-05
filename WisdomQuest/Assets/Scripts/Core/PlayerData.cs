using UnityEngine;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    public int playerLevel { get; private set; } = 1;
    public float experience { get; private set; } = 0f;
    public Dictionary<string, float> knowledgeAreas { get; private set; }
    
    private void Awake()
    {
        knowledgeAreas = new Dictionary<string, float>();
        InitializeKnowledgeAreas();
    }
    
    private void InitializeKnowledgeAreas()
    {
        // Initialize with base knowledge areas
        knowledgeAreas.Add("General", 0f);
    }
    
    public void AddExperience(float amount)
    {
        experience += amount;
        CheckLevelUp();
        UIManager.Instance.UpdateHUD(playerLevel, experience);  // Add the parameters here
    }
    
    public void AddKnowledgeProgress(string area, float amount)
    {
        if (knowledgeAreas.ContainsKey(area))
        {
            knowledgeAreas[area] += amount;
            SaveManager.Instance.SaveGame();
        }
    }
    
    private void CheckLevelUp()
    {
        float experienceNeeded = playerLevel * 100f;
        if (experience >= experienceNeeded)
        {
            playerLevel++;
            experience -= experienceNeeded;
            SaveManager.Instance.SaveGame();
        }
    }
    
    public void LoadSaveData(PlayerSaveData data)
    {
        playerLevel = data.level;
        experience = data.experience;
        knowledgeAreas = data.knowledgeAreas;
    }
}