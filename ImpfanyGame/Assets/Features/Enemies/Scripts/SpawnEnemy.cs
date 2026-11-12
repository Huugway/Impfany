using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement imp;
    [SerializeField] private EnemyMovement bigimp;
    [SerializeField] private PlayerSuperPosition playerSuperPosition;
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private EnemyTracker EnemyTracker;
    

    [ContextMenu("Gegner Spawnen")]
    void SpawnImp()
    {
        EnemyMovement enemy = Instantiate(imp, transform.position, Quaternion.identity);
        enemy.Init(playerSuperPosition);
        EnemyTracker.addEnemy(1);
    }

    void SpawnBigImp()
    {
        EnemyMovement enemy = Instantiate(bigimp, transform.position, Quaternion.identity);
        enemy.Init(playerSuperPosition);
        EnemyTracker.addEnemy(1);
    }

    private void Awake()
    {
        gameFlow.OnWaveStarted += GameFlow_OnWaveStarted;
        EnemyTracker = GetComponent<EnemyTracker>();
    }

    void Update()
    {

    }

    #region Waves
    void Wave(int i)
    {
        switch (i)
        {
            case 1:
                SpawnImp();
                break;
            case 2:
                SpawnBigImp();
                SpawnImp();
                SpawnImp();
                break;
            case 3:
                SpawnBigImp();
                SpawnBigImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                break;
            case 4:
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                break;
            case 5:
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnBigImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                break;
        }
    }
    #endregion
    private void GameFlow_OnWaveStarted(int obj)
    {
        Wave(obj);
    }
}



