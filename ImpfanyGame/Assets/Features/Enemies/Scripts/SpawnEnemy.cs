using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private SuperPosition player;
    [SerializeField] private GameFlow gameFlow;
    [ContextMenu("Gegner Spawnen")]
    private void Awake()
    {
        gameFlow.OnWaveStarted += GameFlow_OnWaveStarted;
    }

    void SpawnEnemy()
    {
        EnemyMovement enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.Init(player);
        Debug.Log("gegner gespawned");
    }


    void Update()
    {
        
    }

    private void GameFlow_OnWaveStarted(int obj)
    {
        SpawnEnemy();
    }
}
