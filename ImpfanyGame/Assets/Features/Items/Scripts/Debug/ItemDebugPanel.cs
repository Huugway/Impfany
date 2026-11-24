using Impfany.Modifiers;
using UnityEngine;

public class ItemDebugPanel : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float spawnDistance = 2f;

    private PlayerMovement player;
    private Rigidbody2D playerBody;
    private IModifiable playerModifiers;
    private Vector2 lastDirection = Vector2.right;
    private GUIStyle labelStyle;
    private GUIStyle buttonStyle;

    private void Update()
    {
        if (player == null) FindPlayer();
        if (player == null) return;

        if (playerBody.linearVelocity.sqrMagnitude > 0.01f)
            lastDirection = playerBody.linearVelocity.normalized;
    }

    private void FindPlayer()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        if (player == null) return;

        playerBody = player.GetComponent<Rigidbody2D>();
        playerModifiers = player.GetComponent<IModifiable>();
    }

    private void OnGUI()
    {
        CreateStyles();
        GUILayout.BeginArea(new Rect(10, Screen.height - 330, 360, 320), GUI.skin.box);
        GUILayout.Label("Item Debug", labelStyle);

        if (player == null)
            GUILayout.Label("Kein Player gefunden", labelStyle);
        else
            DrawPlayerInfo();

        GUILayout.EndArea();
    }

    private void DrawPlayerInfo()
    {
        GUILayout.Label($"MoveSpeed Multiplier: x{playerModifiers.Get(Stat.MoveSpeed):0.00}", labelStyle);
        GUILayout.Label($"Ziel-Speed: {player.GetCurrentSpeed():0.00}", labelStyle);
        GUILayout.Label($"Echte Speed: {playerBody.linearVelocity.magnitude:0.00}", labelStyle);

        GUILayout.Space(10);
        DrawSpawnButton();
    }

    private void DrawSpawnButton()
    {
        if (itemPrefab == null)
        {
            GUILayout.Label("Item Prefab im Inspector setzen", labelStyle);
            return;
        }

        if (GUILayout.Button($"{itemPrefab.name} vor mir spawnen", buttonStyle))
            SpawnItemInFrontOfPlayer();
    }

    private void SpawnItemInFrontOfPlayer()
    {
        Vector2 spawnPosition = (Vector2)player.transform.position + lastDirection * spawnDistance;
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    private void CreateStyles()
    {
        labelStyle ??= new GUIStyle(GUI.skin.label) { fontSize = 18 };
        buttonStyle ??= new GUIStyle(GUI.skin.button) { fontSize = 18 };
    }
}
