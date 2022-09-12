using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [TextArea] public string displayText;
    private DialogManager dialogManagerComponent = null;

    private void Start()
    {
        dialogManagerComponent ??= GetComponent<DialogManager>();
    }
    public void OnPickup()
    {
        dialogManagerComponent?.StartDialog();
    }
    public void OnDrop()
    {
    }
}
