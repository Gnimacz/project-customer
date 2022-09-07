using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField]
    private Transform attachpoint;
    private LayerMask ignoreMask;
    [SerializeField]
    private LayerMask pickupMask;
    [SerializeField]
    private SortingLayer pickupLayer;
    [SerializeField]
    private SortingLayer defaultLayer;
    private GameObject currenthold;
    public bool isHolding = false;


    public void PickUpObject(GameObject pickup)
    {
        if (!pickup.CompareTag("Pickup") && isHolding)
        {
            PutDown(pickup);
        }
        if (pickup.CompareTag("Pickup") && !isHolding)
        {
            PickUp(pickup);

        }
        else if (pickup.CompareTag("Pickup") && isHolding)
        {
            //currenthold.GetComponent<Collider>().enabled = true;
            //currenthold.layer = 0;
            //currenthold.transform.parent = null;
            //Ray floorRay = new Ray(currenthold.transform.position, Vector3.down);
            //RaycastHit floorHit;
            //Physics.Raycast(floorRay, out floorHit, Mathf.Infinity, ~ignoreMask);
            //currenthold.transform.position = floorHit.point;
            //currenthold.transform.rotation = Quaternion.Euler(floorHit.normal.x, currenthold.transform.rotation.y, floorHit.normal.z);
            PutDown(pickup);
            PickUp(pickup);

            //currenthold = pickup;
            //pickup.layer = 6;
            //pickup.GetComponent<Collider>().enabled = false;
            //Vector3.Lerp(pickup.transform.position, attachpoint.transform.position, 1 * Time.deltaTime);
            //pickup.transform.parent = attachpoint;
            //pickup.transform.position = attachpoint.transform.position;
            //pickup.transform.rotation = attachpoint.transform.rotation;
        }
    }

    void PickUp(GameObject obj)
    {
        currenthold = obj;
        isHolding = true;
        obj.layer = 6;
        obj.GetComponent<Collider>().enabled = false;
        //Vector3.Lerp(pickup.transform.position, attachpoint.transform.position, 0.2 * Time.deltaTime);
        obj.transform.parent = attachpoint;
        obj.transform.position = attachpoint.transform.position;
        obj.transform.rotation = attachpoint.transform.rotation;
    }
    void PutDown(GameObject obj)
    {
        currenthold.GetComponent<Collider>().enabled = true;
        currenthold.layer = 0;
        currenthold.transform.parent = null;
        Ray floorRay = new Ray(currenthold.transform.position, Vector3.down);
        RaycastHit floorHit;
        Physics.Raycast(floorRay, out floorHit, Mathf.Infinity, ~ignoreMask);
        currenthold.transform.position = floorHit.point;
        currenthold.transform.rotation = Quaternion.Euler(floorHit.normal.x, currenthold.transform.rotation.y, floorHit.normal.z);
        currenthold = null;
        isHolding = false;
    }
    //void FixedUpdate()
    //{
    //    Spring();
    //}

    //void Spring()
    //{

    //}
}
