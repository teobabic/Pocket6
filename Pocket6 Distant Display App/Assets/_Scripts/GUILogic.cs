using UnityEngine;

// this script is switching between the two cameras in the scene

public class GUILogic : MonoBehaviour
{
    public Camera sceneCamera;
    public Camera interiorAppCamera;
    public MeshRenderer interiorControlSpace;

    private void Start()
    {
        sceneCamera.enabled = false;
        interiorAppCamera.enabled = true;
        interiorControlSpace.enabled = false;
    }

    public void SwitchView()
    {
        if (sceneCamera.enabled)
        {
            sceneCamera.enabled = false;
            interiorAppCamera.enabled = true;
            interiorControlSpace.enabled = false;
        }
        else
        {
            sceneCamera.enabled = true;
            interiorAppCamera.enabled = false;
            interiorControlSpace.enabled = true;
        }
    }
}