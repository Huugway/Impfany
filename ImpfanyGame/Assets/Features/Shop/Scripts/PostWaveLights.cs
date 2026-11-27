using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PostWaveLights : MonoBehaviour
{
    [SerializeField] private Light2D[] lights;

    private GameFlow gameFlow;

    private void Awake()
    {
        gameFlow = FindFirstObjectByType<GameFlow>();
        SetLights(false);
    }

    private void OnEnable()
    {
        if (gameFlow == null) return;
        gameFlow.OnShopOpened += GameFlow_OnShopOpened;
        gameFlow.OnLevelStarted += GameFlow_OnLevelStarted;
    }

    private void OnDisable()
    {
        if (gameFlow == null) return;
        gameFlow.OnShopOpened -= GameFlow_OnShopOpened;
        gameFlow.OnLevelStarted -= GameFlow_OnLevelStarted;
    }

    private void GameFlow_OnShopOpened() => SetLights(true);
    private void GameFlow_OnLevelStarted(int level) => SetLights(false);

    private void SetLights(bool on)
    {
        foreach (Light2D light in lights)
            if (light != null) light.enabled = on;
    }
}