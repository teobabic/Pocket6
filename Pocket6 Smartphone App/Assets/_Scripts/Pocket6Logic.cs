using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// this script includes the main pocket6 algorithm - tracking the smartphone within its control space and remapping the control space upon user-motion.
public class Pocket6Logic : MonoBehaviour
{
    // note, all units are in meters (m).

    // ARKit
    public ARSessionOrigin arSessionOrigin;

    // scene objects
    public GameObject scenePhone;
    public GameObject controlSpace;
    public Vector3 ControlSpaceSize;

    // network objects
    public GameObject networkPhone;

    // Pocket6 algorithm
    private System.Diagnostics.Stopwatch stopwatchForRemapping = new System.Diagnostics.Stopwatch();
    private bool needToBeRemapped = false;

    // gui
    public TMP_Text debugText;

    // touch
    public TouchLogic touchDataLogic;

    private void Start()
    {
        stopwatchForRemapping.Start();
        controlSpace.transform.localScale = ControlSpaceSize;
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            scenePhone.transform.position = arSessionOrigin.camera.transform.position;
            scenePhone.transform.rotation = arSessionOrigin.camera.transform.rotation;
        }

        controlSpace.transform.localScale = ControlSpaceSize;
        DoPocket6();
    }

    private void DoPocket6()
    {
        // phone's position within the control space
        Vector3 distanceFromControlSpaceCenter = controlSpace.transform.InverseTransformPointUnscaled(scenePhone.transform.position);

        float allowedOffsetBeforeRemapping = 0.01f; //1cm

        // check if control space is needed
        if ((Mathf.Abs(distanceFromControlSpaceCenter.x) > (ControlSpaceSize.x / 2) + allowedOffsetBeforeRemapping ||
                Mathf.Abs(distanceFromControlSpaceCenter.y) > (ControlSpaceSize.y / 2) + allowedOffsetBeforeRemapping ||
                Mathf.Abs(distanceFromControlSpaceCenter.z) > (ControlSpaceSize.z / 2) + allowedOffsetBeforeRemapping))
        {
            needToBeRemapped = true;
            stopwatchForRemapping.Reset();
            stopwatchForRemapping.Start();
            RemapControlSpace();
        }

        // wait until the user stops moving (e.g. walking to a different position in the room) and then remap the control space
        // using the stopwatch is optional, the code can be securely removed
        if (stopwatchForRemapping.ElapsedMilliseconds > 1000) //dwell after remapping
        {
            if (needToBeRemapped)
            {
                RemapControlSpace();
                needToBeRemapped = false;
            }
            Vector3 precalculationStep = distanceFromControlSpaceCenter * 100;
            Vector3 positionWithinControlSpace_InProcentageCentered = new Vector3(
                precalculationStep.x / ControlSpaceSize.x,
                precalculationStep.y / ControlSpaceSize.y,
                precalculationStep.z / ControlSpaceSize.z);

            Vector3 positionWithinControlSpace_InProcentage_FromBottomLeft = positionWithinControlSpace_InProcentageCentered + new Vector3(50, 50, 50);
            Vector3 positionWithinControlSpace_Normalized = positionWithinControlSpace_InProcentage_FromBottomLeft / 100; //convert from 0 - 100 to 0.0 to 1.0

            // update the network phone
            networkPhone.transform.position = positionWithinControlSpace_Normalized;
            networkPhone.transform.rotation = arSessionOrigin.camera.transform.rotation;

            // ui output
            debugText.text = "Pocket6 position (norm. xyz): " +
                positionWithinControlSpace_Normalized.x.ToString("F2") + ", " +
                positionWithinControlSpace_Normalized.y.ToString("F2") + ", " +
                positionWithinControlSpace_Normalized.z.ToString("F2") + "\n";

            debugText.text += "World rotation (xyz): " + networkPhone.transform.eulerAngles.x.ToString("F0") + ", " +
                networkPhone.transform.eulerAngles.y.ToString("F0") + ", " +
                networkPhone.transform.eulerAngles.z.ToString("F0") + " °";

            debugText.text += "\nTouch data (state, x, y): " +
                touchDataLogic.GetTouchData().x.ToString("F0") + ", " +
                touchDataLogic.GetTouchData().y.ToString("F0") + ", " +
                touchDataLogic.GetTouchData().z.ToString("F0") + "\n";
        }

    }

    private void RemapControlSpace()
    {
        // this additional if disables control space remapping while the user is performing touch interactions (moving/rotating an object on the distant display)
        if (touchDataLogic.GetTouchData().x == 0)
        {
            controlSpace.transform.position = scenePhone.transform.position;
            controlSpace.transform.eulerAngles = new Vector3(0, scenePhone.transform.eulerAngles.y, 0);
        }
    }

    public void ForceRemapControlSpace()
    {
        // control space remapping is forced after each client or server reconnect
        controlSpace.transform.position = scenePhone.transform.position;
        controlSpace.transform.eulerAngles = new Vector3(0, scenePhone.transform.eulerAngles.y, 0);
    }
}