using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator animator;

    public void Open()
    {
        animator.SetBool("isOpen", true);
    }

    public void Close()
    {
        animator.SetBool("isOpen", false);
    }
    public void ToggleState()
    {
        animator.SetBool("isOpen", !animator.GetBool("isOpen"));
    }
}
