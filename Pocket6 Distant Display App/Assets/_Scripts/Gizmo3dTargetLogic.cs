using System.Collections.Generic;
using UnityEngine;

// this script shows or hides the 3d object highlighting by the white edges of the collider

public class Gizmo3dTargetLogic : MonoBehaviour
{
    public GameObject object3d;
    public GameObject parentObject3d;
    public GameObject gizmoLinesPrefab;
    public LineRenderer lineRendererTopSquare;
    public LineRenderer lineRendererBottomSquare;
    private GameObject visible3dObject;
    private float gizmoEdgeOffset = 0.5f;
    private List<Vector3> allGizmoLinesPositions = new List<Vector3>();
    private List<GameObject> allScaleGizmos = new List<GameObject>();

    void Start()
    {
        GetGizmosScale();
        CreateCornerGizmos();
        visible3dObject = object3d;
        ShowOrHideAllGizmo(false);
    }

    void Update()
    {
        DoLineRendering();
    }

    public GameObject GetVisible3dObject()
    {
        return visible3dObject;
    }

    private void GetGizmosScale()
    {
        Vector3 b = object3d.transform.localScale;

        allGizmoLinesPositions.Add(new Vector3(b.x * gizmoEdgeOffset, b.y / 2, b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(b.x * gizmoEdgeOffset, b.y / 2, -b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(b.x * gizmoEdgeOffset, -b.y / 2, b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(b.x * gizmoEdgeOffset, -b.y / 2, -b.z * gizmoEdgeOffset));

        allGizmoLinesPositions.Add(new Vector3(-b.x * gizmoEdgeOffset, b.y / 2, b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(-b.x * gizmoEdgeOffset, b.y / 2, -b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(-b.x * gizmoEdgeOffset, -b.y / 2, b.z * gizmoEdgeOffset));
        allGizmoLinesPositions.Add(new Vector3(-b.x * gizmoEdgeOffset, -b.y / 2, -b.z * gizmoEdgeOffset));
    }

    private void DoLineRendering()
    {
        lineRendererTopSquare.SetPosition(0, allScaleGizmos[0].transform.position);
        lineRendererTopSquare.SetPosition(1, allScaleGizmos[1].transform.position);
        lineRendererTopSquare.SetPosition(2, allScaleGizmos[5].transform.position);
        lineRendererTopSquare.SetPosition(3, allScaleGizmos[4].transform.position);
        lineRendererTopSquare.SetPosition(4, allScaleGizmos[0].transform.position);

        lineRendererTopSquare.SetPosition(5, allScaleGizmos[2].transform.position);
        lineRendererTopSquare.SetPosition(6, allScaleGizmos[6].transform.position);
        lineRendererTopSquare.SetPosition(7, allScaleGizmos[4].transform.position);

        lineRendererBottomSquare.SetPosition(0, allScaleGizmos[2].transform.position);
        lineRendererBottomSquare.SetPosition(1, allScaleGizmos[3].transform.position);
        lineRendererBottomSquare.SetPosition(2, allScaleGizmos[7].transform.position);
        lineRendererBottomSquare.SetPosition(3, allScaleGizmos[6].transform.position);
        lineRendererBottomSquare.SetPosition(4, allScaleGizmos[2].transform.position);

        lineRendererBottomSquare.SetPosition(5, allScaleGizmos[3].transform.position);
        lineRendererBottomSquare.SetPosition(6, allScaleGizmos[1].transform.position);
        lineRendererBottomSquare.SetPosition(7, allScaleGizmos[5].transform.position);
        lineRendererBottomSquare.SetPosition(8, allScaleGizmos[7].transform.position);
    }

    private void CreateCornerGizmos()
    {
        for (int i = 0; i < allGizmoLinesPositions.Count; i++)
        {
            GameObject gizmoLines = Instantiate(gizmoLinesPrefab, Vector3.zero, parentObject3d.transform.rotation);
            gizmoLines.transform.parent = parentObject3d.transform;
            gizmoLines.transform.localPosition = allGizmoLinesPositions[i];
            gizmoLines.name = "GizmoLines";
            allScaleGizmos.Add(gizmoLines);
        }
    }

    public void ShowOrHideAllGizmo(bool show)
    {
        lineRendererTopSquare.enabled = show;
        lineRendererBottomSquare.enabled = show;
    }
}