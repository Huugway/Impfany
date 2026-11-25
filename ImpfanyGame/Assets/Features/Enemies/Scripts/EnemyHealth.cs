using UnityEngine;
using Impfany.Weapons;
public class EnemyHealth : MonoBehaviour, IEnemyDamageable
{
    [SerializeField] private EnemyTracker EnemyTracker;
    public float health = 50;
    private EnemyHitFlash hitFlash;
    private EnemyMovement movement;

    private void Awake()
    {
        EnemyTracker = FindFirstObjectByType<EnemyTracker>();
        hitFlash = GetComponent<EnemyHitFlash>();
        movement = GetComponent<EnemyMovement>();
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
        return dealt;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;   
        if (EnemyTracker != null)
            EnemyTracker.removeEnemy();
    }
}
