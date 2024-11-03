using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerSaveData playerData;
    public QuestSaveData questData;
    public string timestamp;
}

[Serializable]
public class PlayerSaveData
{
    public int level;
    public float experience;
    public Dictionary<string, float> knowledgeAreas;
}

[Serializable]
public class QuestSaveData
{
    public string[] activeQuestIds;
    public string[] completedQuestIds;
}