using UnityEngine;
using UnityEngine.InputSystem;
using AC;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float gravity = -20f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [SerializeField] private string runAnimationName = "Running_A";
    [SerializeField] private string idleAnimationName = "Idle_A";
    [SerializeField] private string walkAnimationName = "Walking_A";
    [SerializeField] private string attackAnimationName = "Melee_1H_Attack_Stab";
    [SerializeField] private string jumpAnimationName = "Jump_Full_Short";

    [SerializeField] private float animationFadeTime = 0.08f;
    [SerializeField] private float attackFadeTime = 0.03f;
    [SerializeField] private float attackDuration = 0.7f;

    private CharacterController controller;
    private float verticalVelocity;

    private bool isAttacking;
    private bool isJumping;
    private float attackTimer;
    private string currentAnimation;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction?.action.Enable();
        sprintAction?.action.Enable();
        attackAction?.action.Enable();
        jumpAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        sprintAction?.action.Disable();
        attackAction?.action.Disable();
        jumpAction?.action.Disable();
    }

    private void Update()
    {
        if (IsACBlockingControl())
        {
            return;
        }

        HandleAttack();

        if (isAttacking)
        {
            return;
        }

        MovePlayer();
    }

    private bool IsACBlockingControl()
    {
        if (KickStarter.stateHandler == null)
        {
            return false;
        }

        return KickStarter.stateHandler.IsPaused()
            || KickStarter.stateHandler.IsInCutscene();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = Vector2.zero;

        if (moveAction != null)
        {
            moveInput = moveAction.action.ReadValue<Vector2>();
        }

        float horizontal = moveInput.x;
        float vertical = moveInput.y;

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        Vector3 moveDirection = inputDirection;

        if (cameraTransform != null)
        {
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDirection = cameraForward * vertical + cameraRight * horizontal;
        }

        bool isSprinting = false;

        if (sprintAction != null && sprintAction.action.IsPressed())
        {
            isSprinting = true;
        }

        float currentSpeed = walkSpeed;

        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
            isJumping = false;
        }

        if (controller.isGrounded && jumpAction != null && jumpAction.action.WasPressedThisFrame())
        {
            verticalVelocity = jumpForce;
            isJumping = true;

            PlayAnimation(jumpAnimationName, animationFadeTime);
        }

        verticalVelocity += gravity * Time.deltaTime;

        if (!isAttacking && !isJumping)
        {
            UpdateMovementAnimation(moveDirection, isSprinting);
        }

        Vector3 finalMovement = moveDirection * currentSpeed;
        finalMovement.y = verticalVelocity;

        controller.Move(finalMovement * Time.deltaTime);
    }

    private void HandleAttack()
    {
        if (attackAction != null && attackAction.action.WasPressedThisFrame())
        {
            isAttacking = true;
            attackTimer = attackDuration;

            PlayAnimation(attackAnimationName, attackFadeTime);
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
        }
    }

    private void UpdateMovementAnimation(Vector3 moveDirection, bool isSprinting)
    {
        bool isMoving = moveDirection.magnitude > 0.1f;

        if (!isMoving)
        {
            PlayAnimation(idleAnimationName, animationFadeTime);
            return;
        }

        if (isSprinting)
        {
            PlayAnimation(runAnimationName, animationFadeTime);
        }
        else
        {
            PlayAnimation(walkAnimationName, animationFadeTime);
        }
    }

    private void PlayAnimation(string animationName, float fadeTime)
    {
        if (animator == null)
        {
            return;
        }

        if (currentAnimation == animationName)
        {
            return;
        }

        currentAnimation = animationName;
        animator.CrossFadeInFixedTime(animationName, fadeTime);
    }
}