using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement imp;
    [SerializeField] private EnemyMovement bigimp;
    [SerializeField] private PlayerSuperPosition playerSuperPosition;
    [SerializeField] private GameFlow gameFlow;

    [ContextMenu("Gegner Spawnen")]
    void SpawnImp()
    {
        EnemyMovement enemy = Instantiate(imp, transform.position, Quaternion.identity);
        enemy.Init(playerSuperPosition);
    }

    void SpawnBigImp()
    {
        EnemyMovement enemy = Instantiate(bigimp, transform.position, Quaternion.identity);
        enemy.Init(playerSuperPosition);
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
        SpawnBigImp();
        SpawnImp();

    }
}



