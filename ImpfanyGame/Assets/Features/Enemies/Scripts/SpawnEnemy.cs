using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement imp;
    [SerializeField] private EnemyMovement demon;
    [SerializeField] private EnemyMovement bigDemon;
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


    // Zufälliger Punkt in der Arena, aber mindestens 5 Einheiten vom Spieler weg
    Vector3 GetSpawnPosition()
    {
        Vector3 pos;
        do
        {
            int ranX = UnityEngine.Random.Range(-15, 15);
            int ranY = UnityEngine.Random.Range(-15, 15);
            pos = new Vector3(ranX, ranY, 0);
        }
        while (Vector3.Distance(pos, GetPlayer().transform.position) < 5);
        return pos;
    }

    [ContextMenu("Gegner Spawnen")]
    void SpawnImp()
    {
        EnemyMovement enemy = Instantiate(imp, GetSpawnPosition(), Quaternion.identity);
        enemy.Init(GetPlayer());
        EnemyTracker.addEnemy(1);
        EnemyTracker.addEnemyToList(enemy.gameObject);
    }

    void SpawnDemon()
    {
        EnemyMovement enemy = Instantiate(demon, GetSpawnPosition(), Quaternion.identity);
        enemy.Init(GetPlayer());
        EnemyTracker.addEnemy(1);
        EnemyTracker.addEnemyToList(enemy.gameObject);
    }

    void SpawnBigDemon()
    {
        EnemyMovement enemy = Instantiate(bigDemon, GetSpawnPosition(), Quaternion.identity);
        enemy.Init(GetPlayer());
        EnemyTracker.addEnemy(1);
        EnemyTracker.addEnemyToList(enemy.gameObject);
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
                SpawnBigDemon();
                SpawnDemon();
                break;
            case 2:
                SpawnDemon();
                SpawnImp();
                SpawnImp();
                break;
            case 3:
                SpawnDemon();
                SpawnDemon();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                break;
            case 4:
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                SpawnImp();
                break;
            case 5:
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
                SpawnDemon();
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



