using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] Transform tpTarget;
    [SerializeField] Transform player;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = tpTarget.position;
    }

    public void Teleport()
    {
        player.position = tpTarget.position;
    }
}
