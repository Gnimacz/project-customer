using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public DialogueSequence[] dialogues;
    public DialogueOption[] options;
	public DialogueOption optionChosen;

}
[System.Serializable]
public class DialogueSequence
{
	public string name = "";
	public AudioClip voiceLine;
	[TextArea(3, 10)]
	public string sentence;
}
[System.Serializable]
public class DialogueOption
{
	public string name = "";
	public DialogueSequence[] dialogues;
}