using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement imp;
    [SerializeField] private EnemyMovement bigimp;
    private PlayerSuperPosition playerSuperPosition;
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private EnemyTracker EnemyTracker;
    private int level;
    private PlayerSuperPosition GetPlayer()
    {
        if (playerSuperPosition == null)
            playerSuperPosition = FindFirstObjectByType<PlayerSuperPosition>();
        return playerSuperPosition;
    }


    [ContextMenu("Gegner Spawnen")]
    
    void SpawnImp()
    {
        int ranX = UnityEngine.Random.Range(-20, 20);
        int ranY = UnityEngine.Random.Range(-20, 20);
        EnemyMovement enemy = Instantiate(imp, new Vector3(ranX, ranY, 0), Quaternion.identity);
        enemy.Init(GetPlayer());
        EnemyTracker.addEnemy(1);
        EnemyTracker.addEnemyToList(gameObject);
    }
    
    void SpawnBigImp()
    {
        int ranX = UnityEngine.Random.Range(-20, 20);
        int ranY = UnityEngine.Random.Range(-20, 20);
        EnemyMovement enemy = Instantiate(bigimp, new Vector3(ranX, ranY, 0), Quaternion.identity);
        enemy.Init(GetPlayer());
        EnemyTracker.addEnemy(1);
        EnemyTracker.addEnemyToList(gameObject);
    }

    private void Awake()
    {
        gameFlow.OnLevelStarted += GameFlow_OnLevelStarted;
        gameFlow.OnWaveStarted += GameFlow_OnWaveStarted;
        EnemyTracker = GetComponent<EnemyTracker>();
    }

    private void GameFlow_OnLevelStarted(int obj)
    {
        this.level = obj; 
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
        for (int i = 0; i < this.level; i++)
        {
            Wave(obj);
        }
        
    }
}



