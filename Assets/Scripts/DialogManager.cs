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

    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<DialogueSequence> dialogues;
    public UnityEvent DialogueOpened;
    public UnityEvent DialogueClosed;

    private bool displayDialog = true;
    [SerializeField] private GUISkin layout;
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

    //private void OnGUI()
    //{
    //    if (displayDialog)
    //    {
    //        GUI.skin = layout;
    //        GUI.Box(new Rect(Screen.width / 4, Screen.height - 90, Screen.width / 2, Screen.height / 4), "");
    //        GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.02f, Screen.height - 90, Screen.width / 2.2f, 90), dialogueText);
    //        GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.00f, Screen.height - 115, Screen.width / 2.2f, 90), dialogueName);

    //        if (GUI.Button(new Rect(Screen.width/2 + 20, Screen.height - 40, 80, 20), "Continue"))
    //        {
    //            DisplayNextSentence();
    //        }
    //    }
    //}

    public void StartDialogue(Dialogue dialogue)
    {
        foreach (DialogueSequence sequence in dialogue.dialogues)
        {
            dialogues.Enqueue(sequence);
        }
        //change this to work with the actual UI system instead of IMGUI
        //dialogueName = dialogue.name;
        sentences.Clear();
        foreach (DialogueSequence sequence in dialogues)
        {
            sentences.Enqueue(sequence.sentence);
            names.Enqueue(sequence.name);
        }
        dialogues.Clear();

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
        displayDialog = true;
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

    }


    void OnOpen()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        displayDialog = false;
        animator.SetBool("shouldShow", false);
    }


}
