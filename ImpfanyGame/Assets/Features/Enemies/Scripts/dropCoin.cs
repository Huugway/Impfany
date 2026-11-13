using System;
using UnityEngine;

public class dropCoin : MonoBehaviour
{
    [SerializeField] private GameObject prefabCoin;
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        Instantiate(prefabCoin, transform.position, Quaternion.identity);
    }
}
