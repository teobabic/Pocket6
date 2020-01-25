using UnityEngine;

// this script receives the touch data from the smartphone and translates them into TouchStates

public enum TouchStates { Up, OnUp, Drag, OnDown }

public class TouchManager : MonoBehaviour
{
    [HideInInspector]
    public TouchStates touchState;
    [HideInInspector]
    public Vector3 touchData = new Vector3();
    private Vector2 rawTouchXY;
    private int rawTouchState; // 1 = OnDown,  2 = Drag, 0 = Up
    private int prevRawTouchState;

    void Start()
    {
        rawTouchXY = new Vector2(touchData.y, touchData.z);
        rawTouchState = (int) touchData.x;
        prevRawTouchState = rawTouchState;
    }

    void Update()
    {
        rawTouchXY = new Vector2(touchData.y, touchData.z);
        rawTouchState = (int) touchData.x;

        if ((prevRawTouchState == 0 || prevRawTouchState == 1) && rawTouchState == 2)
        {
            touchState = TouchStates.OnDown;
        }
        else if (prevRawTouchState == 2 && rawTouchState == 2)
        {
            touchState = TouchStates.Drag;
        }
        else if ((prevRawTouchState == 2 || prevRawTouchState == 1) && rawTouchState == 0)
        {
            touchState = TouchStates.OnUp;
        }
        else if (prevRawTouchState == 0 && rawTouchState == 0)
        {
            touchState = TouchStates.Up;
        }

        prevRawTouchState = rawTouchState;
    }
}