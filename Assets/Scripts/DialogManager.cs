using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public string dialogueName;
    public string dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueTextUI;
    [SerializeField] private TextMeshProUGUI dialogueNameUI;
    [SerializeField] private Animator animator;

    private Dialogue workingDialogue;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<DialogueSequence> dialogues;
    private List<DialogueOption> options;
    public UnityEvent DialogueOpened;
    public UnityEvent DialogueClosed;

    [SerializeField] private float characterdelay = 0.03f;

    public static DialogManager Dialoguemanager { get; private set; }

    void Awake()
    {
        if (Dialoguemanager != null)
        {
            Destroy(this);
        }
        else
        {
            Dialoguemanager = this;
        }
    }

    private void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        dialogues = new Queue<DialogueSequence>();
        DialogueOpened.AddListener(OnOpen);
        DialogueClosed.AddListener(OnClose);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        workingDialogue = dialogue;
        foreach (DialogueSequence sequence in dialogue.dialogues)
        {
            dialogues.Enqueue(sequence);
        }
        sentences.Clear();
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        dialogues.Clear();
        
        options.Clear();
        foreach (DialogueOption option in dialogue.options)
        {
            options.Add(option);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            Debug.Log("No Sentences found in dialogue");
            EndDialogue();
            return;
        }
        DialogueOpened.Invoke();
        animator.SetBool("shouldShow", true);
        string sentence = sentences.Dequeue();
        dialogueName = names.Dequeue();
        dialogueNameUI.text = dialogueName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText = "";
        foreach (char character in sentence)
        {
            dialogueText += character;
            dialogueTextUI.text = dialogueText;
            yield return new WaitForSeconds(characterdelay);
        }
    }

    void EndDialogue()
    {
        DialogueClosed.Invoke();
        workingDialogue = null;
    }


    void OnOpen()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetBool("shouldShow", false);
    }


}
