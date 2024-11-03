using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button choiceButtonPrefab;
    
    private Queue<DialogueLine> currentDialogue;
    private Action onDialogueComplete;
    private bool isDisplayingDialogue;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currentDialogue = new Queue<DialogueLine>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartDialogue(DialogueSequence sequence, Action onComplete = null)
    {
        currentDialogue.Clear();
        foreach (var line in sequence.lines)
        {
            currentDialogue.Enqueue(line);
        }
        
        onDialogueComplete = onComplete;
        isDisplayingDialogue = true;
        dialoguePanel.SetActive(true);
        DisplayNextLine();
    }
    
    public void DisplayNextLine()
    {
        if (currentDialogue.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        var line = currentDialogue.Dequeue();
        speakerNameText.text = line.speakerName;
        dialogueText.text = line.text;
        
        if (line.choices != null && line.choices.Length > 0)
        {
            ShowChoices(line.choices);
        }
        else
        {
            continueButton.gameObject.SetActive(true);
            choicesPanel.SetActive(false);
        }
    }
    
    private void ShowChoices(DialogueChoice[] choices)
    {
        continueButton.gameObject.SetActive(false);
        choicesPanel.SetActive(true);
        
        // Clear existing choice buttons
        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create new choice buttons
        foreach (var choice in choices)
        {
            var button = Instantiate(choiceButtonPrefab, choicesPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            button.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }
    
    private void OnChoiceSelected(DialogueChoice choice)
    {
        if (choice.onSelected != null)
        {
            choice.onSelected.Invoke();
        }
        
        if (choice.nextSequence != null)
        {
            StartDialogue(choice.nextSequence, onDialogueComplete);
        }
        else
        {
            EndDialogue();
        }
    }
    
    private void EndDialogue()
    {
        isDisplayingDialogue = false;
        dialoguePanel.SetActive(false);
        onDialogueComplete?.Invoke();
    }
}