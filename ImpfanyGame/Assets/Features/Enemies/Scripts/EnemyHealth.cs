using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health = 50;


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

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
