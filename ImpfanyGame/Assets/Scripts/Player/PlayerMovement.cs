using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float movementSpeed = 6f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        moveAction.action.Enable();
    }
    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * movementSpeed;
    }
}
