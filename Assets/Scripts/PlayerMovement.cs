using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public InteractableManager interactableManager;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    [Range(0f, 2f)]
    private float sensitivity = 1.0f;
    //raycast for interactions;
    private RaycastHit interactHit;
    public GameObject hitPart;
    [SerializeField]
    private LayerMask ignoreMask;
    private float xRotation = 0f;
    [Space]
    [Header("Settings")]
    [SerializeField]
    private bool inverted = false;
    [SerializeField, Range(0.0f, 10.0f)] private float interactRayDist = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerRotation();
        if (Input.GetKeyUp(KeyCode.F) && CheckIfInteractable())
        {
            interactableManager.PickUpObject(interactHit.collider.gameObject);
        }
    }

    void FixedUpdate()
    {
        PlayerControls();
        CheckIfInteractable();
    }

    void PlayerControls()
    {
        float movementDirX = Input.GetAxisRaw("Horizontal");
        float movementDirZ = Input.GetAxisRaw("Vertical");
        Vector3 movementDir = transform.forward * movementDirZ + transform.right * movementDirX;
        rb.velocity = movementDir.normalized * moveSpeed;
    }

    void PlayerRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 10 * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 10 * sensitivity;
        transform.Rotate(0, mouseX, 0);
        //cam.transform.Rotate(inverted ? mouseY : -mouseY, 0, 0);
        xRotation += inverted? mouseY : -mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    bool CheckIfInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 5, Color.red);
        Debug.DrawRay(interactHit.point, interactHit.normal * 5, Color.blue);
        return Physics.Raycast(ray, out interactHit, interactRayDist, ~ignoreMask);
    }
}
