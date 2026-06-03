using UnityEngine;
using UnityEngine.InputSystem;
using AC;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Camera Position")]
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] private float distance = 5f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private float yaw;
    private float pitch = 20f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        lookAction?.action.Enable();
    }

    private void OnDisable()
    {
        lookAction?.action.Disable();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (IsACBlockingControl())
        {
            return;
        }

        RotateCamera();
        FollowTarget();
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

    private void RotateCamera()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        bool usingMouse = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero;

        yaw += lookInput.x * mouseSensitivity * Time.deltaTime;
        pitch -= lookInput.y * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 focusPoint = target.position + targetOffset;
        Vector3 cameraPosition = focusPoint - rotation * Vector3.forward * distance;

        transform.position = cameraPosition;
        transform.rotation = rotation;
    }

}