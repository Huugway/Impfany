using UnityEngine;

namespace Impfany.Weapons
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class MeleeConeIndicator : MonoBehaviour
    {
        private const float MaxOpacity = 0.01f;

        [SerializeField]
        private Color tint = new Color(0.72f, 0.72f, 0.72f, 1f);

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Mesh mesh;
        private MaterialPropertyBlock materialProperties;
        private Weapon weapon;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            materialProperties = new MaterialPropertyBlock();
            mesh = new Mesh();
            meshFilter.mesh = mesh;

            weapon = GetComponentInParent<Weapon>();

            BuildMesh(
                weapon.Settings.Range,
                weapon.Settings.AttackAngle);

            SetOpacity(0f);
            weapon.PhaseChanged += OnPhaseChanged;
        }

        private void LateUpdate()
        {
            if (weapon == null)
            {
                Destroy(gameObject);
                return;
            }
            if (weapon.CurrentPhase == WeaponPhase.Recover)
            {
                float recoveryDuration = weapon.Settings.Recover.Duration;

                if (recoveryDuration > 0f)
                {
                    float recoveryProgress = weapon.PhaseElapsed / recoveryDuration;
                    float remaining = 1f - Mathf.Clamp01(recoveryProgress);
                    SetOpacity(MaxOpacity * Mathf.Pow(remaining, 2.5f));
                }

                return;
            }

            if (weapon.CurrentPhase != WeaponPhase.Charge)
            {
                return;
            }

            float duration = weapon.Settings.Charge.Duration;

            if (duration <= 0f)
            {
                return;
            }

            float progress = weapon.PhaseElapsed /duration;
            float curvedProgress = Mathf.Pow(progress, 2.5f);
            SetOpacity(Mathf.Lerp(0f, MaxOpacity, curvedProgress));
        }

        private void OnPhaseChanged(WeaponPhase phase)
        {
            if (phase == WeaponPhase.Charge)
            {
                // Zurück an die Waffe, damit die Vorwarnung mit ihr mitzielt.
                transform.SetParent(weapon.transform, false);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
            }
            else if (phase == WeaponPhase.Recover)
            {
                // Am Ende der Attack-Phase lösen, damit das Nachglühen dort liegen bleibt.
                transform.SetParent(null, true);
            }

            bool striking = phase == WeaponPhase.Attack || phase == WeaponPhase.Recover;
            SetOpacity(striking ? MaxOpacity : 0f);
        }

        private void SetOpacity(float opacity)
        {
            materialProperties.SetColor(
                "_Color",
                new Color(tint.r, tint.g, tint.b, opacity));

            meshRenderer.SetPropertyBlock(materialProperties);
        }

        private void BuildMesh(float range, float attackAngle)
        {
            Vector2[] points = MeleeConeShape.CreatePoints(range, attackAngle);
            Vector3[] vertices = new Vector3[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                vertices[i] = new Vector3(points[i].x, points[i].y, 0f);
            }

            int triangleCount = points.Length - 2;
            int[] triangles = new int[triangleCount * 3];

            for (int i = 0; i < triangleCount; i++)
            {
                int index = i * 3;

                triangles[index] = 0;
                triangles [index + 1] = i + 2;
                triangles[index + 2] = i + 1;
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
        }

        private void OnDestroy()
        {
            if (weapon != null)
            {
                weapon.PhaseChanged -= OnPhaseChanged;
            }

            Destroy(mesh);
        }
    }
}
