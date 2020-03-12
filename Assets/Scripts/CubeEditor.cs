using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof (Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    TextMesh textMesh;

    void Awake() { waypoint = GetComponent<Waypoint>(); }

    void Update() { SnapToGrid(); }

    void SnapToGrid()
    {
        //applies the movement captured in the snapPos vector to be applied to the transform
        //position of the object
        transform.position = new Vector3(waypoint.GetGridPos().x * waypoint.GetGridSize(),
            0f, waypoint.GetGridPos().y * waypoint.GetGridSize());

        SetCubeName();
    }

    void SetCubeName()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textMesh.text = labelText;
        gameObject.name = textMesh.text;
    }
}
