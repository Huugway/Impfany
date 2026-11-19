using UnityEngine;

namespace Impfany.Debuffs
{
    public interface IMovementDebuff
    {
        void Slow(object source, float multiplier, float duration = float.PositiveInfinity, bool cleansable = true);
        void Stun(object source, float duration = float.PositiveInfinity, bool cleansable = true);
        void Remove(object source);
        void Cleanse();
    }
}
