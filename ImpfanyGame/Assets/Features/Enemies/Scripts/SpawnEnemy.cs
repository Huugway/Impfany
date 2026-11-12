using System;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private PlayerSuperPosition player;
    [ContextMenu("Gegner Spawnen")]
    void SpawnEnemy()
    {
        EnemyMovement enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.Init(player);
        Debug.Log("gegner gespawned");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
