using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleLock : MonoBehaviour
{
    private GameObject obj;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    public void Unlock()
    {
        obj.tag = null;
    }

    public void Lock()
    {
        obj.tag = "Interactable";
    }
}

