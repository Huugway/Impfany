using UnityEngine;
using Impfany.Weapons;
public class EnemyHealth : MonoBehaviour, IEnemyDamageable
{
    [SerializeField] private EnemyTracker EnemyTracker;
    public float health = 50;
    private EnemyHitFlash hitFlash;
    private EnemyMovement movement;
    private static readonly int DieParam = Animator.StringToHash("die");
    [SerializeField] private float deathAnimationDuration = 0.7f;
    private Animator animator;
    private bool isDead;
    public bool IsDead => isDead;

    private void Awake()
    {
        EnemyTracker = FindFirstObjectByType<EnemyTracker>();
        hitFlash = GetComponent<EnemyHitFlash>();
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }


    [ContextMenu("Attack ausführen")]
    private void Attack()
    {
        float dealt = ApplyHit(new WeaponHit(10f, 0f, 0f, Vector2.zero));
        Debug.Log($"Dealt {dealt}, health {health}");
    }

    public float ApplyHit(WeaponHit hit)
    {
        if (health <= 0f) return 0f;
        float dealt = Mathf.Min(health, hit.Damage);
        health -= dealt;
        if (hitFlash != null) hitFlash.Play();
        if (movement != null && health > 0f)
{
        if (hit.KnockbackDistance > 0f)
            movement.BeginKnockback(hit.Direction, hit.KnockbackDistance);
            movement.Stun(hit.StunDuration);
}
        if (health <= 0f) Die();
        return dealt;
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null) animator.SetTrigger(DieParam);

        if (movement != null) movement.enabled = false;
        if (TryGetComponent(out AttackPlayer attack)) attack.enabled = false;
        if (TryGetComponent(out Collider2D col)) col.enabled = false;
        if (TryGetComponent(out Rigidbody2D rb)) rb.linearVelocity = Vector2.zero;

        Destroy(gameObject, deathAnimationDuration);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;   
        if (EnemyTracker != null)
            EnemyTracker.removeEnemy();
    }
}
