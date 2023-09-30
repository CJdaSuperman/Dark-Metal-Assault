using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Defines how Waypoint cubes are presented in the Unity editor
    /// </summary>
    [ExecuteInEditMode]
    [SelectionBase]
    [RequireComponent(typeof(Waypoint))]
    public class CubeEditor : MonoBehaviour
    {
        [SerializeField]
        private Waypoint m_waypoint;

        [SerializeField]
        private TextMesh m_label;

        private Vector3 m_newPosition = Vector3.zero;

        private void Awake() 
        {
            if (!m_waypoint)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its {nameof(Waypoint)} component.");

            if (!m_label)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its label.");
        }

        private void Update() => SnapToGrid();

        private void SnapToGrid()
        {
            // Let's say the current waypoint position at x is 6 and the waypoint's grid size is 10.
            // The current waypoint position divided by the waypoint's grid size is 0.6.
            // Then that will round to 1 and multiply by the waypoint's grid size to get
            // that snapping effect without having to hold CTRL to snap objects.
            // If x was 4, then RoundToInt will make it 0 and the object will stay where it is.
            m_newPosition.x = m_waypoint.GetGridPos().x * Waypoint.GridSize;
            m_newPosition.z = m_waypoint.GetGridPos().y * Waypoint.GridSize;

            transform.position = m_newPosition;

            SetCubeName();
        }

        private void SetCubeName()
        {
            m_label.text = $"{m_waypoint.GetGridPos().x}, {m_waypoint.GetGridPos().y}";
            gameObject.name = m_label.text;
        }
    }
}
