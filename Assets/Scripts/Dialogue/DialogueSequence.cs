using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "WisdomQuest/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    public DialogueLine[] lines;
}