using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float walkingSpeed;
    private Camera FPSCam;

   



    private bool isJumping = false;
    private CharacterController charController;


    void Awake()
    {
        charController = GetComponent<CharacterController>();
        FPSCam = gameObject.GetComponentInChildren<Camera>();
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") ;

        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 horizontalMovement = transform.right * horizontalInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + horizontalMovement,1.0f)*walkingSpeed);

        JumpInput();
    }

    private void JumpInput()
    {
        
        if (Input.GetKeyDown("space") && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        float timeInAir = 0f;
        charController.slopeLimit = 90f;

        do
        {
            float jumpSpeed = jumpCurve.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpSpeed * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        }
        while (!charController.isGrounded && charController.collisionFlags!=CollisionFlags.Above);

        isJumping = false;
        charController.slopeLimit = 45f;
    }

    private void Crouch()
    {

        if (Input.GetKey("left ctrl"))
        {

            charController.height = 1.0f;
        }
        else
        {
            charController.height = 2.0f;
        }

    }

    private void LayDown()
    {

        if (Input.GetKey(KeyCode.C))
        {

            while (FPSCam.transform.localPosition.y > 0.4f)
            {
                FPSCam.transform.localPosition = FPSCam.transform.localPosition - Vector3.up * Time.deltaTime;
            }
        }
        else 
        {
            while (FPSCam.transform.localPosition.y < 0.9f)
            {
                FPSCam.transform.localPosition = FPSCam.transform.localPosition + Vector3.up *Time.deltaTime;

            }
            
        }
    }
    void Update()
    {
        Movement();
        Crouch();
        LayDown();
    }
}
