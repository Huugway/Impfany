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
        if (player == null)
        {
            animator.SetBool("isCloseEnoughToHit", false);
            return;
        }
        Updatefacing();
        UpdateHitRange();
    }
    private void FixedUpdate()
    {
        if (player == null) return;
        moveTowardsPlayer();
    }
    void moveTowardsPlayer() 
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
        float dx = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(dx) < HitRange)
        {
            animator.SetBool("isCloseEnoughToHit", true);
        }
        else
        {
            animator.SetBool("isCloseEnoughToHit", false);
        }
    }

}
