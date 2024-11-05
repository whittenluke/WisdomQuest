using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance => instance;
    
    private string SavePath => Path.Combine(Application.persistentDataPath, "wisdomquest.save");
    
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
    
    public void SaveGame()
    {
        var saveData = new SaveData
        {
            playerData = CreatePlayerSaveData(),
            questData = CreateQuestSaveData(),
            timestamp = DateTime.Now.ToString()
        };
        
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        
        Debug.Log($"Game saved to: {SavePath}");
    }
    
    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found.");
            return;
        }
        
        try
        {
            string json = File.ReadAllText(SavePath);
            var saveData = JsonUtility.FromJson<SaveData>(json);
            
            LoadPlayerData(saveData.playerData);
            LoadQuestData(saveData.questData);
            
            Debug.Log($"Game loaded from: {SavePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading save file: {e.Message}");
        }
    }
    
    private PlayerSaveData CreatePlayerSaveData()
    {
        var playerData = FindObjectOfType<PlayerData>();
        return new PlayerSaveData
        {
            level = playerData.playerLevel,
            experience = playerData.experience,
            knowledgeAreas = playerData.knowledgeAreas
        };
    }
    
    private QuestSaveData CreateQuestSaveData()
    {
        var questManager = QuestManager.Instance;
        return new QuestSaveData
        {
            activeQuestIds = questManager.GetActiveQuestIds(),
            completedQuestIds = questManager.GetCompletedQuestIds()
        };
    }
    
    private void LoadPlayerData(PlayerSaveData data)
    {
        var playerData = FindObjectOfType<PlayerData>();
        playerData.LoadSaveData(data);
        UIManager.Instance.UpdateHUD(data.level, data.experience);
    }
    private void LoadQuestData(QuestSaveData data)
    {
        QuestManager.Instance.LoadSaveData(data);
    }
    
    public bool HasSaveFile()
    {
        return File.Exists(SavePath);
    }
    
    public void DeleteSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save file deleted.");
        }
    }
}