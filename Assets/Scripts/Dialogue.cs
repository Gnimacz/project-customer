using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    //public string name = "";

    //[TextArea(3, 10)]
    //public string[] sentences;
    public DialogueSequence[] dialogues;

}
[System.Serializable]
public class DialogueSequence
{
	public string name = "";
	[TextArea(3, 10)]
	public string sentence;
}