using UnityEngine;

// this script maps the humanoid's hand to the smartphone in the scene view

public class HumanoidLogic : MonoBehaviour
{
    public GameObject scenePhone;
    public GameObject sceneInteractionSpace;
    public GameObject humanoidHand;
    public GameObject humanoidBody;

    private Vector3 humanoidPalmPhoneOffset = new Vector3(0.1f, -0.05f, -0.13f); //Offset from Liam's writs to palm-center.
    private Vector3 initialDelta;
    private float humanoidHeight;

    void Start()
    {
        initialDelta = humanoidBody.transform.position - sceneInteractionSpace.transform.position;
        humanoidHeight = humanoidBody.transform.position.y;
    }

    void Update()
    {
        humanoidHand.transform.position = scenePhone.transform.position + humanoidPalmPhoneOffset;
        humanoidHand.transform.rotation = scenePhone.transform.rotation;

        Vector3 newDelta = humanoidBody.transform.position - sceneInteractionSpace.transform.position;
        Vector3 newPosition = humanoidBody.transform.position + (initialDelta - newDelta);
        humanoidBody.transform.position = new Vector3(newPosition.x, humanoidHeight, newPosition.z);

        humanoidBody.transform.rotation = sceneInteractionSpace.transform.rotation;
    }
}