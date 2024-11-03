using UnityEditor;
using UnityEngine;

public class QuestCreator : EditorWindow
{
    [MenuItem("WisdomQuest/Create Tutorial Quest")]
    public static void CreateTutorialQuest()
    {
        // Create the first knowledge piece
        var welcomeKnowledge = ScriptableObject.CreateInstance<KnowledgePiece>();
        welcomeKnowledge.title = "Welcome to the Library";
        welcomeKnowledge.description = "Basic understanding of the Library of Light";
        welcomeKnowledge.area = "General";
        welcomeKnowledge.experienceValue = 10f;
        welcomeKnowledge.timeToLearn = 3f;
        welcomeKnowledge.studyText = "The Library of Light is a mystical place where knowledge takes physical form...";
        
        AssetDatabase.CreateAsset(welcomeKnowledge, 
            "Assets/ScriptableObjects/KnowledgePieces/General/Welcome.asset");
        
        // Create the tutorial quest
        var tutorialQuest = ScriptableObject.CreateInstance<Quest>();
        tutorialQuest.title = "Welcome to WisdomQuest";
        tutorialQuest.description = "Learn about the Library of Light from the Librarian";
        tutorialQuest.experienceReward = 25f;
        tutorialQuest.knowledgeRewards = new KnowledgePiece[] { welcomeKnowledge };
        tutorialQuest.autoCompleteWhenRequirementsMet = true;
        
        AssetDatabase.CreateAsset(tutorialQuest, 
            "Assets/ScriptableObjects/Quests/Tutorial/Welcome.asset");
        
        AssetDatabase.SaveAssets();
    }
} 