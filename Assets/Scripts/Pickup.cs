using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(MeshCollider))]
public class Pickup : MonoBehaviour
{
    MeshRenderer m_Renderer;
    public string description;
    [SerializeField] Material highlightMaterial;
    Material originalMaterial;

    private void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        originalMaterial = m_Renderer.material;
    }

    public void Highlight()
    {
        m_Renderer.material = highlightMaterial;
        m_Renderer.material = originalMaterial;
    }
}
