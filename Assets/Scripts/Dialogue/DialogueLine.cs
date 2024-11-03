using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(3, 10)]
    public string text;
    public DialogueChoice[] choices;
}