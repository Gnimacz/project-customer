using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public Dialogue objectDialogue;


    public UnityEvent OnInteract;
    public UnityEvent OnPutDown;

    public void OnPickup()
    {
        OnInteract.Invoke();
    }
    public void OnDrop()
    {
        OnPutDown.Invoke();
    }
}
