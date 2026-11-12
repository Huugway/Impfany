using System;
using UnityEngine;

public class dropCoin : MonoBehaviour
{
    [SerializeField] private GameObject prefabCoin;
    private void OnDestroy()
    {
        Instantiate(prefabCoin, transform.position, Quaternion.identity);
    }
}
