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
        private readonly HashSet<IEnemyDamageable> damagedTargets = new HashSet<IEnemyDamageable>();

        public override void BeginAttack(WeaponSettings settings)
        {
            if (hitArea == null)
            {
                Debug.LogError("Melee attack needs a Hit Area.", this);
                return;
            }
            BuildHitArea(settings);
            FindTargets();
            ApplyDamage(settings);

            Debug.Log($"Melee cone found {hitResults.Count} colliders.", this);
        }

        public override void EndAttack()
        {
        }

        private void ApplyDamage(WeaponSettings settings)
        {
            damagedTargets.Clear();
            Vector2 origin = hitArea.transform.position;

            foreach (Collider2D col in hitResults)
            {
                IEnemyDamageable target = col.GetComponentInParent<IEnemyDamageable>();
                if (target == null || !damagedTargets.Add(target)) continue;

                Vector2 targetPos = col.attachedRigidbody != null
                    ? col.attachedRigidbody.position
                    : (Vector2)col.transform.position;

                Vector2 direction = targetPos - origin;
                if (direction.sqrMagnitude < 0.0001f)
                    direction = hitArea.transform.right;
                direction.Normalize();

                WeaponHit weaponHit = new WeaponHit(settings.Damage, settings.KnockbackDistance, settings.StunDuration, direction);
                target.ApplyHit(weaponHit);
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