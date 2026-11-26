using Impfany.Modifiers;
using Impfany.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatModifiers))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 80f;
    private Rigidbody2D rb;
    private IModifiable modifiers;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        modifiers = GetComponent<IModifiable>();
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
        Vector2 targetVelocity = Vector2.ClampMagnitude(moveInput, 1f) * GetCurrentSpeed();
        float speedMultiplier = Mathf.Max(1f, modifiers.Get(Stat.MoveSpeed));
        float rate = (IsMoving() ? acceleration : deceleration) * speedMultiplier;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime);
    }

    public float GetCurrentSpeed()
    {
        return movementSpeed * modifiers.Get(Stat.MoveSpeed);
    }

    private bool IsMoving()
    {
        return moveInput.sqrMagnitude > 0.01f;
    }

}
