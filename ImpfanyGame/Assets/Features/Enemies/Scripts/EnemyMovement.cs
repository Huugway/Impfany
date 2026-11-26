using Impfany.Modifiers;
using System.Diagnostics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isInRange;
    [SerializeField] private float HitRange = 0.5f;
    
    private PlayerSuperPosition player = null;
    IModifiable modifiers;
    Transform playerTrans;
    SpriteRenderer spriteRenderer;
    Rigidbody2D ridgidbody;
    Animator animator;
    [SerializeField] private bool facingEast;
    private const float KnockbackDuration = 0.1f;
    private Vector2 knockback;
    private float knockbackTime = KnockbackDuration;
    private float knockbackProgress;
    private float stunnedUntil;
    public bool IsStunned => Time.time < stunnedUntil;
    // ---
    [SerializeField] private float knockdownThreshold = 1f;
    [SerializeField] private float standupDuration = 0.4f;
    private float knockdownUntil;
    // ---

    private void LateUpdate()
    {
        if (player == null)
        {
            PlayerSuperPosition newplayerSuperPosition = FindFirstObjectByType<PlayerSuperPosition>();
            if (newplayerSuperPosition == null) return;
            player = newplayerSuperPosition;
        }
    }
    public void Init(PlayerSuperPosition player)
    {
        this.player = player;
        player.OnMoved += OnPlayerMoved;
    }
    private void Awake()
    {
        modifiers = GetComponent<IModifiable>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ridgidbody = GetComponent<Rigidbody2D>();
    }
    private void OnPlayerMoved(Transform playerTransform)
    {
        playerTrans = playerTransform;
    }
    void Update()
    {
        // do later in debuff
        bool inKnockdown = Time.time < knockdownUntil;
        bool isLying = Time.time < knockdownUntil - standupDuration;
        animator.SetBool("isKnockedDown", isLying);
        animator.SetBool("isStunned", IsStunned && !inKnockdown);
        // ---
        if (player == null)
        {
            animator.SetBool("isCloseEnoughToHit", false);
            return;
        }
        Updatefacing();
        UpdateHitRange();
    }
    // remove if debuff exists
    private void OnDisable()
    {
        animator.SetBool("isStunned", false);
        animator.SetBool("isKnockedDown", false);
    }
    // ---
    private void FixedUpdate()
    {
        if (knockbackTime < KnockbackDuration)
        {
            ApplyKnockback();
            return;
        }

        if (IsStunned)
        {
            ridgidbody.linearVelocity = Vector2.zero;
            return;
        }

        if (player == null) return;
        MoveTowardsPlayer();
    }

    // remove if debuff exists
    public void Stun(float duration)
    {
        stunnedUntil = Mathf.Max(stunnedUntil, Time.time + duration);
        if (duration >= knockdownThreshold)
            knockdownUntil = Mathf.Max(knockdownUntil, Time.time + duration);
    }
    // ---

    public void BeginKnockback(Vector2 direction, float distance)
    {
        knockback = direction * distance;
        knockbackTime = 0f;
        knockbackProgress = 0f;
    }

    private void ApplyKnockback()
    {
        knockbackTime += Time.deltaTime;
        float t = Mathf.Clamp01(knockbackTime / KnockbackDuration);
        float progress = 1f - (1f - t) * (1f - t);
        ridgidbody.MovePosition(ridgidbody.position + knockback * (progress - knockbackProgress));
        knockbackProgress = progress;
    }

    void MoveTowardsPlayer() 
    {
        float speed = movementSpeed * GetSpeedMultiplier();
        Vector2 newPos = Vector2.MoveTowards(ridgidbody.position, player.transform.position, speed * Time.deltaTime);
        ridgidbody.MovePosition(newPos);
    }
    private float GetSpeedMultiplier()
    {
        if (modifiers == null) return 1f;
        return modifiers.Get(Stat.MoveSpeed);
    }
    private void OnDestroy()
    {
        if (player != null)
            player.OnMoved -= OnPlayerMoved;
    }
    void Updatefacing()
    {
        float dx = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(dx) > 0.1f)
            if (dx < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
    }
    void UpdateHitRange()
    {
        isInRange = animator.GetBool("isCloseEnoughToHit");
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (!IsStunned && distance < HitRange)
        {
            animator.SetBool("isCloseEnoughToHit", true);
        }
        else
        {
            animator.SetBool("isCloseEnoughToHit", false);
        }
    }

}
