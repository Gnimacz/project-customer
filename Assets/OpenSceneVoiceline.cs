using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneVoiceline : MonoBehaviour
{
    [SerializeField] Dialogue dia;
    void Start()
    {
        DialogManager.Dialoguemanager.StartDialogue(dia);
    }
}