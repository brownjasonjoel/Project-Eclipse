using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraRoot;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;

    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float cameraPitch;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed +=
            ctx => moveInput = ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled +=
            ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed +=
             ctx => lookInput = ctx.ReadValue<Vector2>();

        controls.Player.Look.canceled +=
            ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        HandleLook();

        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        controller.Move(
            move *
            walkSpeed *
            Time.deltaTime);
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * 0.1f;
        float mouseY = lookInput.y * 0.1f;

        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(
            cameraPitch,
            -85f,
            85f);

        cameraRoot.localRotation =
            Quaternion.Euler(
                cameraPitch,
                0,
                0);

        transform.Rotate(
            Vector3.up * mouseX);
    }
}