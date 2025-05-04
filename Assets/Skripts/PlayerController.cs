using System;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private float gravity = -9.81f*6;
    private float verticalVelocity = 0f;


    public Vector3 floorMotionVector;
    private float ySpeed;
    public float gravity1 = 50;



    private float characterVelocityY;
    private Vector3 characterVelocityMomentum;


    public bool isMoving = false;

    private Animator cameraAnimator;


    private PlayerSoundManager playerSounds;
    private float stepInterval = 1f;
    private float stepTimer;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraAnimator = playerCamera.GetComponent<Animator>();

        playerSounds = transform.GetComponent<PlayerSoundManager>();
        stepTimer = Time.time;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= moveSpeed;

        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);

        bool moving = moveX != 0 || moveZ != 0;

        if (moving != isMoving)
        {
            isMoving = moving;
            cameraAnimator.SetBool("isWalking", isMoving);
        }
       /* floorMotionVector = new Vector3(moveX, 0, moveZ).normalized;
        ySpeed -= gravity1 * Time.deltaTime; // Apply a constant gravity every frame
        floorMotionVector.y = ySpeed; // Apply gravity to your movement vector*/




        if (isMoving && characterController.isGrounded)
        {
            if (stepTimer + 0.85f <= Time.time)
            {
                playerSounds.PlayFootstepSound();
                stepTimer = Time.time;
            }
        }

    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }




    /*private void HandleMovement()
   {
       float moveX = Input.GetAxisRaw("Horizontal");
       float moveZ = Input.GetAxisRaw("Vertical");

       float moveSpeed = 30f;

       Vector3 lastPosition = transform.position;

       Vector3 characterVelocity = (transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed);

       if (characterController.isGrounded)
       {
           //characterVelocityY = 0f;
           // Jump

       }

       // Apply gravity to the velocity
       float gravityDownForce = -110f;
       characterVelocityY += gravityDownForce * Time.deltaTime;


       // Apply Y velocity to move vector
       characterVelocity.y = characterVelocityY;

       // Apply momentum
       characterVelocity += characterVelocityMomentum;

       // Move Character Controller
       characterController.Move(characterVelocity * Time.deltaTime);

       // Dampen momentum
       if (characterVelocityMomentum.magnitude > 0f)
       {
           float momentumDrag = 30f;
           characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
           if (characterVelocityMomentum.magnitude < .0f)
           {
               characterVelocityMomentum = Vector3.zero;
           }
       }

       Vector3 newPosition = transform.position;

       *//*if (newPosition != lastPosition)
       {
           // Moved
           if (!isMoving)
           {
               // Wasn't moving
               isMoving = true;
               OnStartMoving?.Invoke(this, EventArgs.Empty);
           }
       }
       else
       {
           // Didn't move
           if (isMoving)
           {
               // Was moving
               isMoving = false;
               OnStopMoving?.Invoke(this, EventArgs.Empty);
           }
       }
   }*/


















    
    private int ammoCount = 100;

    public bool TryShootAmmo()
    {
        if (ammoCount > 0)
        {
            ammoCount--;
            return true;
        }
        else
        {
            return false;
        }
    }

}
