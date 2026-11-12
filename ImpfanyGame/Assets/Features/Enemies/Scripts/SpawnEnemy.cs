using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private PlayerSuperPosition playerSuperPosition;
    [SerializeField] private GameFlow gameFlow;

    [ContextMenu("Gegner Spawnen")]
    void SpawnEnemy()
    {
        EnemyMovement enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.Init(playerSuperPosition);
        Debug.Log("gegner gespawned");
    }

    private void Awake()
    {
        gameFlow.OnWaveStarted += GameFlow_OnWaveStarted;
    }
    void Update()
    {

    }

    private void GameFlow_OnWaveStarted(int obj)
    {
        SpawnEnemy();
    }
}



