using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [Range(100f, 1000f)]
    public float sensitivity = 200f;

    public Transform playerObject;

    float rotationX = 0f;
    float rotationY = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float mouseMovementX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseMovementY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX = mouseMovementX;

        rotationY -= mouseMovementY;
        rotationY = Mathf.Clamp(rotationY, -85f, 35f);

        transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
        playerObject.Rotate(Vector3.up * rotationX);
    }
}
