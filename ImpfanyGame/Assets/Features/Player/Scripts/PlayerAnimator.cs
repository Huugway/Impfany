using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsMovingParam = Animator.StringToHash("IsMoving");
    private static readonly int WalkDirectionParam = Animator.StringToHash("WalkDirection");

    [SerializeField] private SpriteRenderer spriteRenderer;
    [Tooltip("Ab dieser Geschwindigkeit (Units/Sekunde) gilt der Spieler als laufend.")]
    [SerializeField] private float moveThreshold = 0.1f;
    [Tooltip("So weit (in Units) muss die Maus seitlich vom Spieler entfernt sein, bevor er sich umdreht.")]
    [SerializeField] private float turnDeadZone = 0.05f;

    private Animator animator;
    private Rigidbody2D rb;
    private Camera mainCamera;
    // +1 = schaut nach rechts, -1 = schaut nach links
    private float facing = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Update()
    {
        UpdateFacing();

        Vector2 velocity = rb.linearVelocity;
        bool isMoving = velocity.sqrMagnitude > moveThreshold * moveThreshold;

        // Rueckwaerts = sie laeuft horizontal vom Mauszeiger weg.
        bool movesHorizontally = Mathf.Abs(velocity.x) > moveThreshold;
        bool isWalkingBackwards = isMoving && movesHorizontally && Mathf.Sign(velocity.x) != facing;

        animator.SetBool(IsMovingParam, isMoving);
        animator.SetFloat(WalkDirectionParam, isWalkingBackwards ? -1f : 1f);
    }

    private void UpdateFacing()
    {
        if (Mouse.current != null && mainCamera != null)
        {
            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            float mouseWorldX = mainCamera.ScreenToWorldPoint(mouseScreen).x;
            float offsetX = mouseWorldX - transform.position.x;

            if (Mathf.Abs(offsetX) > turnDeadZone)
            {
                facing = Mathf.Sign(offsetX);
            }
        }

        // Das Sprite schaut im Original nach rechts.
        spriteRenderer.flipX = facing < 0f;
    }
}
