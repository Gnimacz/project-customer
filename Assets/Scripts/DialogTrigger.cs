using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dia;

    public void TriggerDialogue()
    {
        DialogManager.Dialoguemanager.StartDialogue(dia);
    }
}
