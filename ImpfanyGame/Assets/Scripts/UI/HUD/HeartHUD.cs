using UnityEngine;
using UnityEngine.UI;

public class HeartHUD : MonoBehaviour
{
    [SerializeField] private Image heartPrefab;
    [SerializeField] private int testHeartCount = 3;

    private void Start()
    {
        for (int i = 0; i < testHeartCount; i++)
        {
            Instantiate(heartPrefab, transform);
        }
    }
}
