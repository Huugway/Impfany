using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerSuperPosition player;
    Transform playerTrans;
    Rigidbody2D rb;

    public void Init(PlayerSuperPosition player)
    {
        this.player = player;
        player.OnMoved += OnPlayerMoved;
    }
    private void Awake()
    {
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
        moveTowardsPlayer();
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


}
