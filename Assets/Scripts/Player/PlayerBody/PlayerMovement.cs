using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 9.81f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float knockbackForce = 1F;
    public float knockbackDamping = 0.5F;
    public bool isRunning = false;

    public Transform ceilingCheck;
    public float ceilingDistance = 0.2f;
    public LayerMask ceilingMask;
    [SerializeField]
    bool hitCeiling;
    public Transform spawnPoint;

    bool jumpBuffered = false;
    public int maxWallJumps = 1;
    int currentWallJumps = 0;
    bool stuckToWall = false;
    Coroutine jumpingCoroutine;
    
    CharacterController characterController;
    [SerializeField]
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        hitCeiling = Physics.Raycast(ceilingCheck.position, new Vector3(0, 1, 0), ceilingDistance, ceilingMask);


        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);



        if ((Input.GetButtonDown("Jump") || jumpBuffered) && canMove && characterController.isGrounded || (Input.GetButtonDown("Jump") || jumpBuffered) && stuckToWall && canMove)
        {
            moveDirection.y = jumpSpeed;
            jumpBuffered = false;
            stuckToWall = false;
            if(jumpingCoroutine != null) StopCoroutine(jumpingCoroutine);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (Input.GetButtonDown("Jump") && canMove && !characterController.isGrounded) {
            StartCoroutine(jumpBuffer());
        }


        if (!characterController.isGrounded && hitCeiling) {
            moveDirection.y = -knockbackForce;
            moveDirection.x = knockbackForce * -Mathf.Sign(moveDirection.x);
            moveDirection.z = knockbackForce * -Mathf.Sign(moveDirection.z);

            // Apply damping to force
            moveDirection *= knockbackDamping;
        }

        if(characterController.isGrounded){
            currentWallJumps = 0;
        }
        


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        
        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void Respawn(){
        characterController.enabled = false;
        characterController.transform.position = spawnPoint.position;
        characterController.enabled = true;
    }

    

    IEnumerator jumpBuffer(){
        jumpBuffered = true;
        yield return new WaitForSeconds(0.1F);
        jumpBuffered = false;   
    }
}