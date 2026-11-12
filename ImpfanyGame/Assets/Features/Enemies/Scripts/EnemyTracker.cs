using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public event System.Action OnWaveEnd;
    [SerializeField] private int enemyCount;

    public void addEnemy(int i)
    {
        enemyCount += i;
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
