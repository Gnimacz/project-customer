using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager storyManager { get; private set; }


    void Awake()
    {
        if (storyManager != null)
        {
            Destroy(this);
        }
        else
        {
            storyManager = this;
        }
    }

}
