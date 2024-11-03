using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Knowledge Piece", menuName = "WisdomQuest/Knowledge Piece")]
public class KnowledgePiece : ScriptableObject
{
    public string title;
    public string description;
    public string area;
    public float experienceValue;
    public Sprite icon;
    public float timeToLearn = 5f;
    [TextArea(3, 10)]
    public string studyText;
}