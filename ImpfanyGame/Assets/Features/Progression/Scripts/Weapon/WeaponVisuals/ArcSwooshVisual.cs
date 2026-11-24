using UnityEngine;

namespace Impfany.Weapons
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class ArcSwooshVisual : WeaponVisual
    {
        private static readonly int RangeId = Shader.PropertyToID("_Range");
        private static readonly int AngleId = Shader.PropertyToID("_Angle");
        private static readonly int ProgressId = Shader.PropertyToID("_Progress");

        private Mesh mesh;
        private MeshRenderer meshRenderer;
        private MaterialPropertyBlock materialProperties;

        [SerializeField]
        private int sortingOrder = 10;

        protected override void Awake()
        {
            base.Awake();

            if (Weapon == null)
            {
                return;
            }

            mesh = BuildCoverMesh(Weapon.Settings.Range);
            GetComponent<MeshFilter>().mesh = mesh;

            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sortingOrder = sortingOrder;
            meshRenderer.enabled = false;

            materialProperties = new MaterialPropertyBlock();
        }

        protected override void OnPhaseChanged(WeaponPhase phase)
        {
            meshRenderer.enabled = phase == WeaponPhase.Attack;
        }

        private void LateUpdate()
        {
            if (Weapon.CurrentPhase == WeaponPhase.Attack)
            {
                Apply(Weapon.PhaseProgress);
            }
        }

        private void Apply(float progress)
        {
            materialProperties.SetFloat(RangeId, Weapon.Settings.Range);
            materialProperties.SetFloat(AngleId, Weapon.Settings.AttackAngle);
            materialProperties.SetFloat(ProgressId, progress);
            meshRenderer.SetPropertyBlock(materialProperties);
        }

        private static Mesh BuildCoverMesh(float range)
        {
            float half = range + 0.25f;

            Mesh coverMesh = new Mesh();
            coverMesh.vertices = new[]
            {
                new Vector3(-half, -half, 0f),
                new Vector3(-half, half, 0f),
                new Vector3(half, half, 0f),
                new Vector3(half, -half, 0f)
            };
            coverMesh.triangles = new[] { 0, 1, 2, 0, 2, 3 };
            coverMesh.RecalculateBounds();

            return coverMesh;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Destroy(mesh);
        }
    }
}