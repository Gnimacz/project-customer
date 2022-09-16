using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public Dialogue objectDialogues;

    private void Start()
    {
        gameObject.tag = "Interactable";
    }

    public UnityEvent OnInteract;
    public UnityEvent OnPutDown;

    [Tooltip("Can the object be picked up?")] public bool canBePickedUp = true;

    public void OnPickup()
    {
        OnInteract.Invoke();
    }
    public void OnDrop()
    {
        OnPutDown.Invoke();
    }

    public void ShowDialogue()
    {
        DialogManager.Dialoguemanager.StartDialogue(objectDialogues);
    }

    public void SetScale()
    {
        StoryManager.storyManager.storybeats.FindBeatByName("TestBeat").OnCompleteStoryBeat();
    }
}
