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
    private GameObject currenthold;
    public bool isHolding = false;
    private RaycastHit interactHit = new RaycastHit();
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float interactRayDist = 5f;
    [SerializeField]
    private Material interactMaterial;
    Material oldMaterial;
    private bool canPickup = false;
    private GameObject lastHit;

    #region Input
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && canPickup)
        {
            PickUpObject(interactHit.collider.gameObject);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            PutDown(currenthold);
        }
    }

    private void FixedUpdate()
    {
        canPickup = CheckIfInteractable();
    }
    bool CheckIfInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactRayDist, Color.red);
        Debug.DrawRay(interactHit.point, interactHit.normal * interactRayDist, Color.blue);
        bool didHit = Physics.Raycast(ray, out interactHit, interactRayDist, ~ignoreMask);
        return didHit;
    }
    #endregion

    #region Pick up and Put down objects
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

            PutDown(pickup);
            PickUp(pickup);

        }
    }

    void PickUp(GameObject obj)
    {
        currenthold = obj;
        isHolding = true;
        obj.layer = 6;
        obj.GetComponent<Collider>().enabled = false;
        obj.transform.parent = attachpoint;
        obj.transform.position = attachpoint.transform.position;
        obj.transform.rotation = attachpoint.transform.rotation;
    }
    void PutDown(GameObject obj)
    {
        currenthold.GetComponent<Collider>().enabled = true;
        currenthold.layer = 0;
        currenthold.transform.parent = null;
        if (canPickup)
        {
            currenthold.transform.position = interactHit.point;
        }
        Ray floorRay = new Ray(currenthold.transform.position, Vector3.down);
        RaycastHit floorHit = new RaycastHit();
        if (interactHit.normal != Vector3.up)
        {
            Physics.Raycast(floorRay, out floorHit, Mathf.Infinity, ~ignoreMask);
            currenthold.transform.position = floorHit.point;

        }


        currenthold.transform.rotation = Quaternion.Euler(floorHit.normal.x, currenthold.transform.rotation.y, floorHit.normal.z);
        currenthold = null;
        isHolding = false;
    }
    #endregion

}