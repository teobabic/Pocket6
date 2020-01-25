using UnityEngine;

// this script uses the cursor to detect a movable 3d object (collision detection) and the performs its translation and rotation

public class CursorLogic : MonoBehaviour
{
    // object highlighting
    public Material materialObject3dDefault;
    public Material materialObject3dHighlighted;
    private GameObject currentlyHighlightedObject3d;
    private GameObject lastHighlightedObject3d;

    // grabbing
    private bool isGrabbed = false;
    private Vector3 interactableGameObjectPositionOnGrab;
    private Vector3 cursorPositionOnGrab;

    // touch rotation
    private TouchManager touchManager;
    private float touchXOnGrab;
    private Vector3 objectRotationOnGrab;

    void Start()
    {
        touchManager = GameObject.Find("Interaction Logic").GetComponent<TouchManager>();
    }

    void Update()
    {
        if (isGrabbed && currentlyHighlightedObject3d != null)
        {
            TranslateAndRotateObjectBy3dMovementAndTouch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGrabbed)
        {
            SelectObject3d(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isGrabbed)
        {
            DeHighlightLastSelectedObject();
            lastHighlightedObject3d = currentlyHighlightedObject3d;
        }
    }

    private void SelectObject3d(GameObject collidedObject3d)
    {
        currentlyHighlightedObject3d = collidedObject3d;
        DeHighlightLastSelectedObject();
        lastHighlightedObject3d = currentlyHighlightedObject3d;
        HighlightSelectedObject();
    }

    public void Grab()
    {
        if (currentlyHighlightedObject3d != null)
        {
            // a interactable object was grabbed
            isGrabbed = true;
            interactableGameObjectPositionOnGrab = currentlyHighlightedObject3d.transform.parent.transform.position;
            cursorPositionOnGrab = transform.position;
            touchXOnGrab = touchManager.touchData.y;
            objectRotationOnGrab = currentlyHighlightedObject3d.transform.parent.transform.eulerAngles;
        }
    }

    public void Release()
    {
        isGrabbed = false;
    }

    public void TranslateAndRotateObjectBy3dMovementAndTouch()
    {
        // translation the 3d object by phone translation
        Vector3 differenceCursorAndObject = transform.position - cursorPositionOnGrab;
        currentlyHighlightedObject3d.transform.parent.transform.position = interactableGameObjectPositionOnGrab + differenceCursorAndObject;

        // rotation the 3d object by touch swipes
        float diffTouchX = touchManager.touchData.y - touchXOnGrab;
        float rotationSpeed = diffTouchX / 2;
        currentlyHighlightedObject3d.transform.parent.transform.eulerAngles = new Vector3(
            currentlyHighlightedObject3d.transform.parent.transform.eulerAngles.x,
            objectRotationOnGrab.y + rotationSpeed,
            currentlyHighlightedObject3d.transform.parent.transform.eulerAngles.z
        );
    }

    private void HighlightSelectedObject()
    {
        foreach (Transform child in lastHighlightedObject3d.transform)
        {
            child.GetComponent<MeshRenderer>().material = materialObject3dHighlighted;
        }
        lastHighlightedObject3d.transform.parent.GetComponent<Gizmo3dTargetLogic>().ShowOrHideAllGizmo(true);
    }

    private void DeHighlightLastSelectedObject()
    {
        if (lastHighlightedObject3d != null)
        {
            foreach (Transform child in lastHighlightedObject3d.transform)
            {
                child.GetComponent<MeshRenderer>().material = materialObject3dDefault;
            }
            lastHighlightedObject3d.transform.parent.GetComponent<Gizmo3dTargetLogic>().ShowOrHideAllGizmo(false);
        }
    }
}