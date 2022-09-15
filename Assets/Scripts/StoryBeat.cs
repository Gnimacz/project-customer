using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StoryBeat : MonoBehaviour
{
    public string beatName;
    public bool isComplete = false;
    public Event OnComplete;
    
    public void OnGetStoryBeat()
    {
        
    }

    void OnCompleteStoryBeat()
    {

    }

}


[System.Serializable]
public class StoryBeatList
{
    public StoryBeat[] storyBeatsInScene;
}
