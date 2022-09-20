using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLock : MonoBehaviour
{
    public bool isLocked;

    private void Start()
    {
        if (isLocked) Lock();
    }

    public void Lock()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }
    public void Unlock()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
