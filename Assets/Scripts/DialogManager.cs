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
    [SerializeField] private TextMeshProUGUI firstOptionName;
    [SerializeField] private TextMeshProUGUI secondOptionName;
    [SerializeField] private TextMeshProUGUI thirdOptionName;
    [SerializeField] private Animator dialogueAnimator;
    [SerializeField] private Animator optionsAnimator;

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
        options = new List<DialogueOption>();
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
        names.Clear();
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        dialogues.Clear();

        options.Clear();
        if (workingDialogue.options.Length > 0)
        {
            foreach (DialogueOption option in dialogue.options)
            {
                options.Add(option);
            }
        }
        Debug.Log(options);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        optionsAnimator.SetBool("Show", false);
        if (workingDialogue.optionChosen != null)
        {
            foreach (DialogueSequence dialogue in workingDialogue.optionChosen.dialogues)
            {
                names.Enqueue(dialogue.name);
                sentences.Enqueue(dialogue.sentence);
            }
            //workingDialogue = null;
        }
        if (sentences.Count == 0 && options.Count > 0)
        {
            optionsAnimator.SetBool("Show", true);
            firstOptionName.text = options[0].name;
            secondOptionName.text = options[1].name;
            thirdOptionName.text = options[2].name;
            Debug.Log("Switching to options");
            return;
        }
        if (sentences.Count == 0)
        {
            Debug.Log("No Sentences found in dialogue");
            EndDialogue();
            return;
        }
        DialogueOpened.Invoke();
        dialogueAnimator.SetBool("shouldShow", true);
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

        options.Clear();
        sentences.Clear();
        dialogues.Clear();
        names.Clear();
    }


    void OnOpen()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        dialogueAnimator.SetBool("shouldShow", false);
    }

    public void Option1()
    {
        Debug.Log("Option 1");
        //workingDialogue.optionChosen = options[0];
        foreach (DialogueSequence item in options[0].dialogues)
        {
            dialogues.Enqueue(item);
        }
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        optionsAnimator.SetBool("Show", false);
        options.Clear();
        DisplayNextSentence();
    }
    public void Option2()
    {
        Debug.Log("Option 2");
        //workingDialogue.optionChosen = options[1];
        foreach (DialogueSequence item in options[1].dialogues)
        {
            dialogues.Enqueue(item);
        }
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        optionsAnimator.SetBool("Show", false);
        options.Clear();
        DisplayNextSentence();
    }
    public void Option3()
    {
        Debug.Log("Option 3");
        //workingDialogue.optionChosen = options[2];
        foreach (DialogueSequence item in options[2].dialogues)
        {
            dialogues.Enqueue(item);
        }
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        optionsAnimator.SetBool("Show", false);
        options.Clear();
        DisplayNextSentence();
    }

}
