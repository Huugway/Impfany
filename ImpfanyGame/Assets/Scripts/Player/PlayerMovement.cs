using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 80f;
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
        Vector2 targetVelocity = Vector2.ClampMagnitude(moveInput, 1f) * movementSpeed;
        float rate = moveInput.sqrMagnitude > 0.01f ? acceleration : deceleration;
        rb.linearVelocity = Vector2.MoveTowards(
        rb.linearVelocity,
        targetVelocity,
        rate * Time.fixedDeltaTime);
    }
}
