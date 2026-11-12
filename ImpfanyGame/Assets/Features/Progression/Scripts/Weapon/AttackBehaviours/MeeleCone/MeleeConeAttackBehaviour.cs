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
        private readonly HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();

        public override void BeginAttack(WeaponSettings settings)
        {
            if (hitArea == null)
            {
                Debug.LogError("Melee attack needs a Hit Area.", this);
                return;
            }
            BuildHitArea(settings);
            FindTargets();
            ApplyDamage(settings.Damage);

            Debug.Log($"Melee cone found {hitResults.Count} colliders.", this);
        }

        public override void EndAttack()
        {
        }

        private void ApplyDamage(float damage)
        {
            damagedTargets.Clear();
            
            foreach (Collider2D hit in hitResults)
            {
                IDamageable target = hit.GetComponentInParent<IDamageable>();
            
                if (target != null && damagedTargets.Add(target))
                {
                    target.TakeDamage(damage);
                }
            }
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
            Vector2[] points = MeleeConeShape.CreatePoints(settings.Range, settings.AttackAngle);
            
            hitArea.pathCount = 1;
            hitArea.SetPath(0, points);
        }
    }
}