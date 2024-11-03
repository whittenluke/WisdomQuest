using UnityEngine;
using System.Collections.Generic;

public class KnowledgeManager : MonoBehaviour
{
    private static KnowledgeManager instance;
    public static KnowledgeManager Instance => instance;
    
    private Dictionary<string, List<KnowledgePiece>> knowledgeDatabase;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeDatabase()
    {
        knowledgeDatabase = new Dictionary<string, List<KnowledgePiece>>();
    }
    
    public void AddKnowledgePiece(KnowledgePiece piece)
    {
        if (!knowledgeDatabase.ContainsKey(piece.area))
        {
            knowledgeDatabase[piece.area] = new List<KnowledgePiece>();
        }
        knowledgeDatabase[piece.area].Add(piece);
    }
    
    public List<KnowledgePiece> GetKnowledgePieces(string area)
    {
        return knowledgeDatabase.ContainsKey(area) ? knowledgeDatabase[area] : new List<KnowledgePiece>();
    }
}