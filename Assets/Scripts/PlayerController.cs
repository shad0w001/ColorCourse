using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Transform playerCamera;
    private PlayerAnimationController animationController;
    PlayerInput playerInput;

    //Ground Movement Variables
    private Vector2 input;
    private Vector3 direction;
    [SerializeField] float speed = 10f;
    private bool isMovementPressed;
    public (Vector3, Quaternion) respawnPosition;

    //Sprinting Variables
    public bool isSprinting;
    public float multiplier;
    public float acceleration;
    [HideInInspector] private float currentSpeed;
    private bool isSprintingPressed;

    //Gravity Variables
    private float earthGravity = -9.81f;
    [SerializeField] float gravityMultiplier = 1f;
    private float velocity;

    //Jump Variables
    [SerializeField]public float jumpHeight = 6f;
    private bool isJumpPressed;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animationController = GetComponentInChildren<PlayerAnimationController>();

        playerInput.GameplayActions.Move.started += onMovementButtonPress;
        playerInput.GameplayActions.Move.performed += onMovementButtonPress;
        playerInput.GameplayActions.Move.canceled += onMovementButtonPress;

        playerInput.GameplayActions.Sprint.started += onSprintButtonPress;
        playerInput.GameplayActions.Sprint.canceled += onSprintButtonPress;
    }

    private void Start()
    {
        respawnPosition = (transform.position, transform.rotation);
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        Physics.SyncTransforms();
        velocity = 0f;
        respawnPosition = (position, rotation);
    }
    public void RespawnPlayer()
    {
        Teleport(respawnPosition.Item1, respawnPosition.Item2);
    }

    private void Update()
    {
        ApplyMovement();
        ApplyAnimations();
        ApplyGravity();
    }

    
    private void ApplyMovement()
    {
        UpdateMovementDirection();

        float targetSpeed;

        if (isSprinting)
        {
            targetSpeed = speed * multiplier;
        }
        else
        {
            targetSpeed = speed;
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 totalMovement = (direction * (currentSpeed)) + Vector3.up * velocity;
        characterController.Move(totalMovement * Time.deltaTime);
    }
    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity < 0f)
        {
            velocity = -1f;
        }
        else
        {
            velocity += earthGravity * gravityMultiplier * Time.deltaTime;
            direction.y = velocity;
        }
    }
    private void ApplyAnimations()
    {
        bool isWalking = animationController.animator.GetBool("isWalking");
        bool isSprinting = animationController.animator.GetBool("isSprinting");
        bool isJumping = animationController.animator.GetBool("isJumping");

        if(isJumpPressed && !isJumping && !characterController.isGrounded)
        {
            animationController.StopWalking();
            animationController.StartJumping();
        }
        else if(isJumping && characterController.isGrounded)
        {
            animationController.StopJumping();
        }

        if (isMovementPressed && !isWalking && !isSprintingPressed)
        {
            animationController.StartWalking();
        }
        else if(!isMovementPressed && isWalking)
        {
            animationController.StopWalking();
        }

        if (isSprintingPressed && isMovementPressed && !isSprinting && !isJumping)
        {
            animationController.StopWalking();
            animationController.StartSprinting();
        }
        else if ((!isMovementPressed || !isSprintingPressed) && isSprinting)
        {
            animationController.StopSprinting();
        }
    }
    //input handlers
    private void onMovementButtonPress(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        isMovementPressed = input.x != 0 || input.y != 0;
    }
    private void onSprintButtonPress(InputAction.CallbackContext context)
    {
        isSprintingPressed = context.ReadValueAsButton();
        isSprinting = context.started || context.performed;
    }
    private void onJumpButtonPress(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        velocity += jumpHeight;
    }

    //these are the methods from the input manager
    public void Move(InputAction.CallbackContext context)
    {
        onMovementButtonPress(context);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started || !characterController.isGrounded)
        {
            return;
        }
        onJumpButtonPress(context);
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        onSprintButtonPress(context);
    }

    //Helper methods
    private void UpdateMovementDirection()
    {
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        forward.y = 0f;
        right.y = 0f;

        direction = forward * input.y + right * input.x;
    }

    //enable/disable methods for the input action schema
    private void OnEnable() => playerInput.GameplayActions.Enable();
    private void OnDisable() => playerInput.GameplayActions.Disable();
}
