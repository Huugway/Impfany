using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public event System.Action OnWaveEnd;
    [SerializeField] private int enemyCount;
    private List<GameObject> enemyList;

    public void addEnemy(int i)
    {
        enemyCount += i;

    }

    public void addEnemyToList(GameObject enemy)
    {
        enemyList.Add(enemy);
    }
    public void removeAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            Destroy(enemy);
        }
    }

    public void removeEnemy()
    {
        enemyCount--;
    }

    private void checkEnemyCount()
    {
        if (enemyCount == 0)
        {
            OnWaveEnd?.Invoke();
        }
        else return;
    }
    void Update()
    {
        checkEnemyCount();
    }
}
