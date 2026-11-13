using UnityEngine;

public class DeleteCoin : MonoBehaviour
{
    GameFlow gameFlow;
    private void Awake()
    {

        gameFlow.OnStatusChanged += GameFlow_OnStatusChanged;
    }

    private void GameFlow_OnStatusChanged(GameStatus obj)
    {
        if (obj.Equals(GameStatus.StartMenu))
        {
            Destroy(gameObject);
        }
        
    }
}
