using System;
using UnityEngine;

public class PlayerSuperPosition : MonoBehaviour
{
    public event Action<Transform> OnMoved;
   
    void Update()
    {
        OnMoved?.Invoke(transform);
    }
}
