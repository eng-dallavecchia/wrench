using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] Transform bodyTransform;


    private float yLimitAngle = 0f;
    private float walkingSpeed = 20f;
    


    void Awake()
    {
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    } 
    private void CameraRotation()
    {
        float mouseXAngle = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseYAngle= Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;


        yLimitAngle += mouseYAngle;

        if (yLimitAngle<90f && yLimitAngle > -90f)
        {
            transform.Rotate(Vector3.left* mouseYAngle);
        }
        else if (yLimitAngle >= 90f)
        {
            yLimitAngle = 90f;
        }
        else if(yLimitAngle<=-90f)
        {
            yLimitAngle = -90f;
        }

        bodyTransform.Rotate(Vector3.up * mouseXAngle);
    }

 
    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }
}
