using System;
using UnityEngine;

public class dropCoin : MonoBehaviour
{
    [SerializeField] private GameObject prefabCoin;
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        // Nur droppen, wenn der Gegner getötet wurde, nicht beim Aufräumen (Restart/Sieg)
        if (!TryGetComponent(out EnemyHealth health) || !health.IsDead) return;
        Instantiate(prefabCoin, transform.position, Quaternion.identity);
    }
}
