using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class StoryBeatList : MonoBehaviour
{
    public List<StoryBeat> storyBeatsInScene;

    public StoryBeat FindBeatByName(string beatName)
    {
        foreach (StoryBeat beat in storyBeatsInScene)
        {
            if (beat.name == beatName)
            {
                return beat;
            }
        }
        Debug.LogError("No StoryBeat can be found with that name!");
        return null;
    }
}

[System.Serializable]
public class StoryBeat
{
    public string name;
    public bool isComplete = false;
    public StoryBeat prerequisite;
    public UnityEvent OnComplete;
    public UnityEvent OnGetEvent;

    public void OnGetStoryBeat()
    {
        OnGetEvent.Invoke();
    }

    public void OnCompleteStoryBeat()
    {
        OnComplete.Invoke();
    }

}

