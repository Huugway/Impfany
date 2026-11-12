using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAim : MonoBehaviour
{
    [Tooltip("So weit (in Units) muss die Maus seitlich vom Spieler entfernt sein, bevor der Holder die Seite wechselt.")]
    [SerializeField] private float turnDeadZone = 0.05f;

    private readonly List<SpriteRenderer> weaponSprites = new List<SpriteRenderer>();
    private Camera mainCamera;
    private float baseOffsetX;
    // +1 = Holder rechts vom Spieler, -1 = links
    private float side = 1f;

    private void Awake()
    {
        mainCamera = Camera.main;
        baseOffsetX = Mathf.Abs(transform.localPosition.x);
    }

    private void Update()
    {
        if (Mouse.current == null || mainCamera == null) return;

        Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Seite relativ zum Spieler bestimmen, nicht zum Holder, sonst flackert es.
        float offsetX = mouseWorld.x - transform.parent.position.x;
        if (Mathf.Abs(offsetX) > turnDeadZone) side = Mathf.Sign(offsetX);

        Vector3 localPosition = transform.localPosition;
        localPosition.x = baseOffsetX * side;
        transform.localPosition = localPosition;

        // Waffen zeigen im Original nach rechts (+X).
        Vector2 aimDirection = mouseWorld - (Vector2)transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Ohne flipY stünde die Waffe beim Zielen nach links auf dem Kopf.
        bool pointsLeft = aimDirection.x < 0f;
        GetComponentsInChildren(weaponSprites);
        foreach (SpriteRenderer sprite in weaponSprites) sprite.flipY = pointsLeft;
    }
}