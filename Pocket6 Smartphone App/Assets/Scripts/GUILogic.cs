using UnityEngine;

// this script manages GUI buttons and functions

public class GUILogic : MonoBehaviour
{
    public GameObject sceneCamera;
    public GameObject controlSpace;
    public GameObject buttonRecenterCamera;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // prevent smartphone app from falling asleep

        // initial view preparation
        sceneCamera.SetActive(false);
        buttonRecenterCamera.SetActive(false);
    }

    public void SwitchView()
    {
        // for the ui button - change view
        if (sceneCamera.activeSelf)
        {
            sceneCamera.SetActive(false);
            buttonRecenterCamera.SetActive(false);
        }
        else
        {
            sceneCamera.SetActive(true);
            buttonRecenterCamera.SetActive(true);
        }
    }

    public void RecenterSceneCamera()
    {
        // for the ui button - recenter camera
        sceneCamera.transform.position = controlSpace.transform.TransformPointUnscaled(new Vector3(0, 1.5f, -3f));
        sceneCamera.transform.eulerAngles = new Vector3(30f, controlSpace.transform.eulerAngles.y, controlSpace.transform.eulerAngles.z);
    }
}

public static class TransformExtensions
{
    public static Vector3 TransformPointUnscaled(this Transform transform, Vector3 position)
    {
        var localToWorldMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        return localToWorldMatrix.MultiplyPoint3x4(position);
    }
    public static Vector3 InverseTransformPointUnscaled(this Transform transform, Vector3 position)
    {
        var worldToLocalMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one).inverse;
        return worldToLocalMatrix.MultiplyPoint3x4(position);
    }
}