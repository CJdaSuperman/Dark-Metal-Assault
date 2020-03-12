using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float scrollSpeed = 5f;
    [SerializeField] float windowEdgeBorder = 10f;

    [Header("Camera Limits")]
    [SerializeField] float xPositionLeftLimit;
    [SerializeField] float xPositionRightLimit;
    [SerializeField] float zUpPositionLimit;
    [SerializeField] float zBottomPositionLimit;
    [SerializeField] float zoomOutLimit;
    [SerializeField] float zoomInLimit;    

    bool movementEnabled = true;

    GameManager gameManager;

    void Awake() => gameManager = FindObjectOfType<GameManager>();

    void Update()
    {
        if (gameManager.IsGameOver())
        {
            enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            movementEnabled = !movementEnabled;

        if (!movementEnabled)
            return;

        MoveCamera();
    }    

    void MoveCamera()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - windowEdgeBorder)
            position.z += panSpeed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= windowEdgeBorder)
            position.z -= panSpeed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= windowEdgeBorder)
            position.x -= panSpeed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - windowEdgeBorder)
            position.x += panSpeed * Time.deltaTime;        

        position = ClampPan(position);

        position = ScrollWheelMove(position);

        transform.position = position;
    }    

    Vector3 ClampPan(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, xPositionLeftLimit, xPositionRightLimit);
        position.z = Mathf.Clamp(position.z, zBottomPositionLimit, zUpPositionLimit);
        return position;
    }

    Vector3 ScrollWheelMove(Vector3 position)
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, zoomInLimit, zoomOutLimit);

        return position;
    }
}
