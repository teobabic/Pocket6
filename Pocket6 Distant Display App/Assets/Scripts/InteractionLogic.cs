using UnityEngine;

// this script controls the cursor its position within the interior scene and triggers touch event

public class InteractionLogic : MonoBehaviour
{
    public GameObject cursor;
    public GameObject distantDisplayInteractionSpace;
    public CursorLogic cursorLogic3D;
    private bool objectGrabbed = false;
    public Transform scenePhone;
    public NetworkLogic networkLogic;
    public TouchManager touchManager;
    private GameObject pocket6Phone;

    void Update()
    {
        if (pocket6Phone == null)
        {
            pocket6Phone = GameObject.Find("PhonePocket6");
        }
        else
        {
            // scene interior cursor
            Vector3 pocket6PositionNormalized = pocket6Phone.transform.position;
            Vector3 cursorNewPosition = distantDisplayInteractionSpace.transform.TransformPoint(new Vector3(-1, -1, -1) / 2); // left, back, bottom corner of the distant display interaction space

            cursorNewPosition.x += pocket6PositionNormalized.x * distantDisplayInteractionSpace.transform.localScale.x;
            cursorNewPosition.y += pocket6PositionNormalized.y * distantDisplayInteractionSpace.transform.localScale.y;
            cursorNewPosition.z += pocket6PositionNormalized.z * distantDisplayInteractionSpace.transform.localScale.z;

            //cursor.transform.position = cursorNewPosition; // no lerping
            cursor.transform.position = Vector3.Lerp(cursor.transform.position, cursorNewPosition, Mathf.PingPong(Time.time, 1.0f));

            if (touchManager.touchState == TouchStates.Drag && !objectGrabbed)
            {
                // grab
                objectGrabbed = true;
                cursorLogic3D.Grab();
            }

            if (touchManager.touchState == TouchStates.OnUp)
            {
                // on up - release
                objectGrabbed = false;
                cursorLogic3D.Release();
            }

            // adjust the scene phone position within the scene control space (debug view)
            scenePhone.localPosition = (new Vector3(-1, -1, -1) / 2) + pocket6Phone.transform.position;
        }
    }
}