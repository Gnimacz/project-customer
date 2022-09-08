using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [TextArea] public string displayText;

    public void OnPickup()
    {

    }
    public void OnDrop()
    {
    }
}
