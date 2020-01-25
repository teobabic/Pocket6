using UnityEngine;

// this script listens to touch inputs and prepares them for the network object

public class TouchLogic : MonoBehaviour
{
    private Vector3 touchData;

    public Vector3 GetTouchData()
    {
        return touchData;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.y > 0)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchData = new Vector3(1, touch.position.x, touch.position.y); // 1 = OnDown
                        break;

                    case TouchPhase.Moved:
                        touchData = new Vector3(2, touch.position.x, touch.position.y); // 2 = Drag
                        break;

                    case TouchPhase.Ended:
                        touchData = new Vector3(0, touch.position.x, touch.position.y); // 0 = Up (finger is not touching the screen)
                        break;
                }
            }
        }
    }
}