using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerSuperPosition player;
    Transform playerTrans;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator am;
    [SerializeField] private bool facingEast;

    Vector2 newPos;
    [SerializeField] private bool isInRange;

    [SerializeField] private float HitRange = 0.5f;
    public void Init(PlayerSuperPosition player)
    {
        this.player = player;
        player.OnMoved += OnPlayerMoved;
    }
    private void Awake()
    {
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerSuperPosition>();
    }
    private void OnPlayerMoved(Transform playerTransform)
    {
        playerTrans = playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        Updatefacing();
        moveTowardsPlayer();
        UpdateHitRange();
    }

    void moveTowardsPlayer() 
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, player.transform.position, movementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
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
                sr.flipX = true;
            else
                sr.flipX = false;
    }

    void UpdateHitRange()
    {
        isInRange = am.GetBool("isCloseEnoughToHit");
        float dx = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(dx) < HitRange)
        {
            am.SetBool("isCloseEnoughToHit", true);
        }
        else
        {
            am.SetBool("isCloseEnoughToHit", false);
        }
    }
}
