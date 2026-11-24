using System;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    private GameFlow gameFlow;
    private GameObject spawnedItem;

    private void Awake()
    {
        gameFlow = FindFirstObjectByType<GameFlow>();
    }
    private void LateUpdate()
    {
        gameFlow.OnShopOpened += GameFlow_OnShopOpened;
    }

    private void GameFlow_OnShopOpened()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (spawnedItem != null) return;

        spawnedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

    }
}
