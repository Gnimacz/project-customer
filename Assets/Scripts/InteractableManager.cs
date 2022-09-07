using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField]
    private Transform attachpoint;
    [SerializeField]
    private LayerMask pickupLayer;
    private GameObject currenthold;
    private bool isHolding = false;

    public void PickUpObject(GameObject pickup)
    {
        if (pickup.CompareTag("Pickup") && isHolding)
        {
            currenthold = pickup;
            pickup.GetComponent<Collider>().enabled = false;
            Vector3.Lerp(pickup.transform.position, attachpoint.transform.position, 1);
            pickup.transform.position = attachpoint.transform.position;
            pickup.transform.rotation = attachpoint.transform.rotation;
            pickup.transform.parent = attachpoint;
            pickup.layer = pickupLayer;

        }
        else if (pickup.CompareTag("Pickup") && isHolding)
        {
            currenthold.GetComponent<Collider>().enabled = false;
            currenthold.layer = 0;


            pickup.GetComponent<Collider>().enabled = false;
            Vector3.Lerp(pickup.transform.position, attachpoint.transform.position, 1);
            pickup.transform.position = attachpoint.transform.position;
            pickup.transform.rotation = attachpoint.transform.rotation;
            pickup.transform.parent = attachpoint;
            pickup.layer = pickupLayer;
        }
    }

    //void FixedUpdate()
    //{
    //    Spring();
    //}

    //void Spring()
    //{

    //}
}
