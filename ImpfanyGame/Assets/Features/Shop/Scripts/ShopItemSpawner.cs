using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private ItemBuff[] itemPool;

    private GameFlow gameFlow;
    private readonly List<ItemBuff> spawnedItems = new List<ItemBuff>();

    private void Awake()
    {
        gameFlow = FindFirstObjectByType<GameFlow>();
    }

    private void OnEnable()
    {
        if (gameFlow != null) gameFlow.OnShopOpened += GameFlow_OnShopOpened;
    }

    private void OnDisable()
    {
        if (gameFlow != null) gameFlow.OnShopOpened -= GameFlow_OnShopOpened;
    }

    private void GameFlow_OnShopOpened()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        ClearItems();

        List<ItemBuff> pool = new List<ItemBuff>(itemPool);
        if (pool.Count == 0) return;

        foreach (Transform slot in slots)
        {
            // Pool leer? Wieder auffüllen -> erst wenn es zu wenig Items gibt, gibt es Doppelte.
            if (pool.Count == 0) pool.AddRange(itemPool);

            int index = Random.Range(0, pool.Count);
            ItemBuff item = Instantiate(pool[index], slot.position, Quaternion.identity);
            pool.RemoveAt(index);

            item.OnPickedUp += Item_OnPickedUp;
            spawnedItems.Add(item);
        }
    }

    private void Item_OnPickedUp(ItemBuff pickedItem)
    {
        spawnedItems.Remove(pickedItem);
        ClearItems();
    }

    private void ClearItems()
    {
        foreach (ItemBuff item in spawnedItems)
        {
            if (item == null) continue;
            item.OnPickedUp -= Item_OnPickedUp;
            Destroy(item.gameObject);
        }
        spawnedItems.Clear();
    }
}
