using Impfany.Debuffs;
using UnityEngine;

[RequireComponent(typeof(MovementDebuff))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isInRange;
    [SerializeField] private float HitRange = 0.5f;
    
    private PlayerSuperPosition player = null;
    MovementDebuff movementDebuff;
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
        moveTowardsPlayer();
        UpdateHitRange();
    }
    void moveTowardsPlayer() 
    {
        Vector2 newPos = Vector2.MoveTowards(ridgidbody.position, player.transform.position, movementSpeed * Time.fixedDeltaTime * movementDebuff.SpeedMultiplier);
        ridgidbody.MovePosition(newPos);
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
