using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerDebuff : MonoBehaviour
{
    private struct Debuff
    {
        public float SpeedMultipier;
        public bool Stuns;
        public bool Cleansable;
        public float Duration;
    }
    private readonly Dictionary<object, Debuff> debuffs = new();
    private readonly List<object> expired = new();

    public float SpeedMultipiler { get; private set; } = 1f;
    public bool IsStunned { get; private set; }

    public void Slow(object source, float multiplier, float duration = float.PositiveInfinity)
        => Apply(source, multiplier, false, duration);
    public void Stun(object source, float duration = float.PositiveInfinity)
        => Apply(source, 1f, true, duration);
    public void Apply(object source, float multiplier, bool stuns, float duration)
    {
        debuffs[source] = new Debuff()
        {
            SpeedMultipier = Mathf.Max(0f, multiplier),
            Stuns = stuns,
            Duration = duration
        };
        Calculate();
    }
    public void Calculate()
    {
        SpeedMultipiler = 1f;
        IsStunned = false;
        foreach (var debuff in debuffs.Values)
        {
            SpeedMultipiler *= debuff.SpeedMultipier;
            IsStunned |= debuff.Stuns;
        }
    }

    void Update()
    {
        foreach (var pair in debuffs)
            if (Time.time >= pair.Value.Duration) expired.Add(pair.Key);

        if (expired.Count == 0) return;
        
        foreach (var source in expired) debuffs.Remove(source);
        expired.Clear();
        Calculate();
    }
}
