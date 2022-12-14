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
    [SerializeField] private TextMeshProUGUI continueButton;
    [SerializeField] private Animator dialogueAnimator;
    [SerializeField] private Animator optionsAnimator;
    [SerializeField] private Queue<AudioClip> sounds;
    [SerializeField] private AudioSource voiceSource;

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
        sounds = new Queue<AudioClip>();
        DialogueOpened.AddListener(OnOpen);
        DialogueClosed.AddListener(OnClose);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        firstOptionName.transform.parent.gameObject.SetActive(false);
        secondOptionName.transform.parent.gameObject.SetActive(false);
        thirdOptionName.transform.parent.gameObject.SetActive(false);
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
            sounds.Enqueue(sequence.voiceLine);
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
        DialogueOpened.Invoke();
        continueButton.transform.parent.gameObject.SetActive(true);
        optionsAnimator.SetBool("Show", false);
        if (workingDialogue.optionChosen != null)
        {
            foreach (DialogueSequence dialogue in workingDialogue.optionChosen.dialogues)
            {
                names.Enqueue(dialogue.name);
                sentences.Enqueue(dialogue.sentence);
                sounds.Enqueue(dialogue.voiceLine);
                Debug.Log($"Sound queue: {sounds.Count}" );
            }
            //workingDialogue = null;
        }
        if (sentences.Count == 0 && options.Count > 0)
        {
            continueButton.transform.parent.gameObject.SetActive(false);
            optionsAnimator.SetBool("Show", true);
            if (workingDialogue.options.Length >= 1)
            {
                firstOptionName.transform.parent.gameObject.SetActive(true);
                firstOptionName.text = options[0].name;
            }
            if (workingDialogue.options.Length >= 2)
            {
                secondOptionName.transform.parent.gameObject.SetActive(true);
                secondOptionName.text = options[1].name;
            }
            if (workingDialogue.options.Length >= 3)
            {
                thirdOptionName.transform.parent.gameObject.SetActive(true);
                thirdOptionName.text = options[2].name;
            }
            Debug.Log("Switching to options");
            return;
        }
        if (sentences.Count == 0)
        {
            Debug.Log("No Sentences found in dialogue");
            EndDialogue();
            return;
        }
        dialogueAnimator.SetBool("shouldShow", true);
        string sentence = sentences.Dequeue();
        dialogueName = names.Dequeue();
        dialogueNameUI.text = dialogueName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        voiceSource.Stop();
        AudioClip voiceLine = sounds.Dequeue();
        if(voiceLine != null) voiceSource.PlayOneShot(voiceLine);

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
        sounds.Clear();
    }


    void OnOpen()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        dialogueAnimator.SetBool("shouldShow", false);
        firstOptionName.transform.parent.gameObject.SetActive(false);
        secondOptionName.transform.parent.gameObject.SetActive(false);
        thirdOptionName.transform.parent.gameObject.SetActive(false);
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
            sounds.Enqueue(sequence.voiceLine);
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
            sounds.Enqueue(sequence.voiceLine);
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
            sounds.Enqueue(sequence.voiceLine);
        }
        optionsAnimator.SetBool("Show", false);
        options.Clear();
        DisplayNextSentence();
    }

}
