using UnityEngine;
using UnityEngine.UI;

public class HeartHUD : MonoBehaviour
{
    [SerializeField] private Image heartPrefab;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private int testHeartCount = 3;
    [SerializeField] private int testFilledHearts = 2;

    private void Start()
    {
        for (int i = 0; i < testHeartCount; i++)
        {
            Image heart = Instantiate(heartPrefab, transform);

            if (i < testFilledHearts)
            {
                heart.sprite = fullHeartSprite;
            } else
            {
                heart.sprite = emptyHeartSprite;
            }
        }
    }
}
