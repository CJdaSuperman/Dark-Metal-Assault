using Managers;
using System;
using UnityEngine;

/// <summary>
/// Controls the movement of the camera
/// </summary>
public class CameraController : MonoBehaviour
{
    [Serializable]
    private struct CoordinateRange
    {
        public float min;
        public float max;
    }

    [Header("Camera Attributes")]
    
    [SerializeField] 
    private float m_panSpeed;

    [SerializeField]
    private float m_scrollSpeed;

    [Header("Camera Limits")]
    
    [SerializeField] 
    private float m_windowBoundryOffset;

    [SerializeField]
    private CoordinateRange m_xRange;

    [SerializeField]
    private CoordinateRange m_zoomRange;

    [SerializeField]
    private CoordinateRange m_zRange;

    private Vector3 m_currentPosition;
    private Vector3 m_mousePosition;

    private bool m_movementEnabled = true;

    private void Update()
    {
        if (InputManager.LockCamera())
            m_movementEnabled = !m_movementEnabled;

        if (!m_movementEnabled)
            return;

        MoveCamera();
    }    

    private void MoveCamera()
    {
        m_currentPosition = transform.position;
        m_mousePosition = Input.mousePosition;
        float panDelta = m_panSpeed * Time.deltaTime;

        // Pan up
        if (InputManager.PanUp() || m_mousePosition.y >= Screen.height - m_windowBoundryOffset)
            m_currentPosition.z += panDelta;

        // Pan down
        if (InputManager.PanDown() || m_mousePosition.y <= m_windowBoundryOffset)
            m_currentPosition.z -= panDelta;

        // Pan left
        if (InputManager.PanLeft() || m_mousePosition.x <= m_windowBoundryOffset)
            m_currentPosition.x -= panDelta;

        // Pan right
        if (InputManager.PanRight() || m_mousePosition.x >= Screen.width - m_windowBoundryOffset)
            m_currentPosition.x += panDelta;

        ClampPan();
        Zoom(InputManager.Scroll());

        transform.position = m_currentPosition;
    }
    
    private void ClampPan()
    {
        m_currentPosition.x = Mathf.Clamp(m_currentPosition.x, m_xRange.min, m_xRange.max);
        m_currentPosition.z = Mathf.Clamp(m_currentPosition.z, m_zRange.min, m_zRange.max);
    }

    private void Zoom(float scroll)
    {
        if (scroll != 0f)
        {
            m_currentPosition.y -= scroll * m_scrollSpeed * Time.deltaTime;
            m_currentPosition.y = Mathf.Clamp(m_currentPosition.y, m_zoomRange.min, m_zoomRange.max);
        }
    }
}
