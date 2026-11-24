namespace Impfany.Modifiers
{
    // Alles, was Buffs und Debuffs bekommen kann (Player, Gegner).
    // Buff = multiplier > 1 (z. B. 1.15 = +15 %), Debuff = multiplier < 1 (z. B. 0.5 = halb so schnell).
    public interface IModifiable
    {
        void Apply(object source, Stat stat, float multiplier, float duration = float.PositiveInfinity, bool cleansable = true);
        void Remove(object source);
        void Cleanse();
        float Get(Stat stat);
    }
}
