using UnityEngine;

namespace Impfany.Weapons
{
    public static class MeleeConeShape
    {
        public static Vector2[] CreatePoints(float range, float attackAngle)
        {
            const int segments = 16;
            Vector2[] points = new Vector2[segments +2];

            points[0] = Vector2.zero;

            float halfAngle = attackAngle * 0.5f;

            for (int i = 0; i <= segments; i++)
            {
                float progress = i / (float)segments;
                float angle = Mathf.Lerp(-halfAngle, halfAngle, progress);
                float radians = angle * Mathf.Deg2Rad;

                points[i + 1] = new Vector2(
                    Mathf.Cos(radians),
                    Mathf.Sin(radians)
                ) * range;
            }
            return points;
        }
    }
}
