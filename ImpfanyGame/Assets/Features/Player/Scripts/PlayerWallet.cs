using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField]private int coins;

    internal void AddCoin()
    {
        coins++;
    }

}
