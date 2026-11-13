using UnityEngine;
using Impfany.Weapons;
public class EnemyHealth : MonoBehaviour, IEnemyDamageable
{
    [SerializeField] private EnemyTracker EnemyTracker;
    public float health = 50;

    private void Awake()
    {
        EnemyTracker = FindFirstObjectByType<EnemyTracker>();
    }


    [ContextMenu("Attack ausführen")]
    private void Attack()
    {
        TakeDamage(10);
        Debug.Log(health.ToString());
    }

    void TakeDamage(float amount)
    {
        health -= amount;
    }


    void IEnemyDamageable.TakeDamage(float amount)
    {
        TakeDamage(amount);
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
