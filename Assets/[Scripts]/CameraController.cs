// CameraController.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 1000.0f;
    public Transform playerBody;

    private float Xrotation = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGamePlaying)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            Xrotation -= mouseY;
            Xrotation = Mathf.Clamp(Xrotation, -90.0f, 90.0f);

            transform.localRotation = Quaternion.Euler(Xrotation, 0.0f, 0.0f);
            playerBody.Rotate(Vector3.up * mouseX);
            //playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
