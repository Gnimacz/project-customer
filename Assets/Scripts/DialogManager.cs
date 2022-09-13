using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public string dialogueName;
    public string dialogueText;
    private Queue<string> sentences;
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
        DialogueOpened.AddListener(OnOpen);
        DialogueClosed.AddListener(OnClose);
    }

    private void OnGUI()
    {
        if (displayDialog)
        {
            GUI.skin = layout;
            GUI.Box(new Rect(Screen.width / 4, Screen.height - 90, Screen.width / 2, Screen.height / 4), "");
            GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.02f, Screen.height - 90, Screen.width / 2.2f, 90), dialogueText);
            GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.00f, Screen.height - 115, Screen.width / 2.2f, 90), dialogueName);

            if (GUI.Button(new Rect(Screen.width / 4 + Screen.width * 0.42f, Screen.height - 50, Screen.width / 6f, Screen.height - Screen.height * 0.0002f), "Continue"))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //change this to work with the actual UI system instead of IMGUI
        dialogueName = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            Debug.Log("No Sentences found in dialogue");
            return;
        }
        DialogueOpened.Invoke();
        displayDialog = true;
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText = "";
        foreach (char character in sentence)
        {
            dialogueText += character;
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
        Debug.Log("A");
    }

    void OnClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        displayDialog = false;
    }


}
