// CameraController.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float controlSensitivity = 1000.0f;
    public Transform playerBody;
    public Joystick rightJoyStick;

    private float Xrotation = 0.0f;

    private void Start()
    {
        GameObject joystick = GameObject.Find("/GameUICanvas/OnScreenControls/Right Fixed Joystick");
        if(joystick != null)
        {
            rightJoyStick = joystick.GetComponent<Joystick>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGamePlaying)
        {
            float horizontal;
            float vertical;
            
                horizontal = rightJoyStick.Horizontal * controlSensitivity * Time.deltaTime;
                vertical = rightJoyStick.Vertical * controlSensitivity * Time.deltaTime;
            
                //horizontal = Input.GetAxis("Mouse X") * controlSensitivity * Time.deltaTime;
                //vertical = Input.GetAxis("Mouse Y") * controlSensitivity * Time.deltaTime;
            
            

            Xrotation -= vertical;
            Xrotation = Mathf.Clamp(Xrotation, -90.0f, 90.0f);

            transform.localRotation = Quaternion.Euler(Xrotation, 0.0f, 0.0f);
            playerBody.Rotate(Vector3.up * horizontal);
            //playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
