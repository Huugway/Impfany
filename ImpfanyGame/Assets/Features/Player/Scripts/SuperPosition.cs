using System;
using UnityEngine;

public class SuperPosition : MonoBehaviour
{
    public event Action<Transform> OnMoved;
    
   
    // Update is called once per frame
    void Update()
    {
        OnMoved?.Invoke(transform);
    }
}
