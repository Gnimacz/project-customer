using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private Transform attachpoint;
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private string pickupTag = "Interactable";
    [SerializeField] private Camera cam;
    [SerializeField] private float interactRayDist = 5f;
    [SerializeField] private LayerMask ignoreMask;
    private GameObject currenthold;
    private Transform objectScale;
    public bool isHolding = false;
    private RaycastHit interactHit = new RaycastHit();

    [SerializeField] Material oldMaterial;
    [SerializeField] Material highLightMaterial;
    private Transform selectedObject;
    private bool canPickup = false;
    public bool allowPickup = true;

    #region Input and Detection
    private void Update()
    {


        HandleSelection();
        if (Input.GetKeyUp(KeyCode.F) && allowPickup)
        {
            HandleInteraction();
        }


        //if (Input.GetKeyUp(KeyCode.F) && canPickup)
        //{
        //    PickUpObject(interactHit.collider.gameObject);
        //}
        //else if (Input.GetKeyUp(KeyCode.F) && allowPickup && currenthold != null)
        //{
        //    PutDown(currenthold);
        //}

    }

    public void PickUpAllowed() { allowPickup = true; }
    public void PickUpBlocked() { allowPickup = false; }

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

    #region Interaction
    void HandleInteraction()
    {
        if (canPickup && interactHit.collider.gameObject != null)
        {
            GameObject interactable = interactHit.collider.gameObject;
            Interactable obj = interactable.GetComponent<Interactable>();

            obj?.OnPickup();
            if (isHolding && currenthold != null)
            {
                PutDown(currenthold);
            }
            if (obj != null && obj.canBePickedUp)
            {
                PickUpObject(interactable);
            }

        }

        else if (isHolding && currenthold != null)
        {
            PutDown(currenthold);
        }

    }
    #endregion

    #region Pick up and Put down objects
    public void PickUpObject(GameObject pickup)
    {

        if (!pickup.CompareTag(pickupTag) && isHolding)
        {
            PutDown(pickup);
        }
        if (pickup.CompareTag(pickupTag) && !isHolding)
        {
            PickUp(pickup);

        }
        else if (pickup.CompareTag(pickupTag) && isHolding)
        {

            PutDown(pickup);
            PickUp(pickup);

        }
    }

    void PickUp(GameObject obj)
    {
        currenthold = obj;
        objectScale = obj.transform;
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
        //var test = currenthold.GetComponent<Mesh>().bounds.extents;
        if (interactHit.normal != Vector3.up)
        {
            Physics.Raycast(floorRay, out floorHit, Mathf.Infinity, ~ignoreMask);
            currenthold.transform.position = floorHit.point;

        }

        currenthold.transform.localScale = objectScale.transform.localScale;
        currenthold.transform.rotation = Quaternion.Euler(floorHit.normal.x, currenthold.transform.rotation.y, floorHit.normal.z);
        currenthold = null;
        isHolding = false;
        obj.GetComponent<Interactable>()?.OnDrop();
    }
    #endregion

    #region Highlight selected object
    void HandleSelection()
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponent<Renderer>();
            selectionRenderer.material = oldMaterial;
            selectedObject = null;
        }

        if (canPickup)
        {
            var selection = interactHit.transform;
            if (selection.CompareTag(pickupTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    if (oldMaterial == null || (oldMaterial != selectionRenderer.material && oldMaterial != highLightMaterial))
                    {
                        oldMaterial = selectionRenderer.material;
                    }
                    selectionRenderer.material = highLightMaterial;
                }

                selectedObject = selection;
            }
        }
    }
    #endregion

}
