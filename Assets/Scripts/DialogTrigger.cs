using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogManager manager;

    private void OnTriggerEnter(Collider other)
    {
        manager.StartDialog();
    }
}
