using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public DialogueSequence nextSequence;
    public UnityEvent onSelected;
}