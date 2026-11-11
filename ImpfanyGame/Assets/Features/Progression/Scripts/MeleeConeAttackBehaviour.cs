using System.Collections.Generic;
using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class MeleeConeAttackBehaviour : WeaponAttackBehaviour
    {
        [SerializeField]
        private PolygonCollider2D hitArea;
        [SerializeField]
        private LayerMask targetLayers;
        private readonly List<Collider2D> hitResults = new List<Collider2D>();

        public override void BeginAttack(WeaponSettings settings)
        {
            if (hitArea == null)
            {
                Debug.LogError("Melee attack needs a Hit Area.", this);
                return;
            }
            BuildHitArea(settings);
            FindTargets();

            Debug.Log($"Melee cone found {hitResults.Count} colliders.", this);
        }

        public override void EndAttack()
        {
        }

        private void FindTargets()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(targetLayers);
            filter.useTriggers = true;

            hitResults.Clear();
            hitArea.enabled = true;

            try
            {
                Physics2D.SyncTransforms();
                hitArea.Overlap(filter, hitResults);
            }
            finally
            {
                hitArea.enabled = false;
            }
        }

        private void BuildHitArea(WeaponSettings settings)
        {
            const int segments = 16;
            Vector2[] points = new Vector2[segments +2];

            points[0] = Vector2.zero;

            float halfAngle = settings.AttackAngle * 0.5f;

            for (int i = 0; i <= segments; i++)
            {
                float progress = i / (float)segments;
                float angle = Mathf.Lerp(-halfAngle, halfAngle, progress);
                float radians = angle * Mathf.Deg2Rad;

                points[i + 1] = new Vector2(
                    Mathf.Cos(radians),
                    Mathf.Sin(radians)
                ) * settings.Range;
            }
            
            hitArea.pathCount = 1;
            hitArea.SetPath(0, points);
        }
    }
}