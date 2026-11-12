using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    public int GetEnemyCount()
    {
        return enemyCount;
    }
    public void addEnemy(int i)
    {
        enemyCount += i;
    }
    public void removeEnemy()
    {
        enemyCount--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
