using System.Collections.Generic;
using UnityEngine;

public class PlayerDebuff : MonoBehaviour
{
    private struct Debuff
    {
        public float SpeedMultiplier;
        public bool Stuns;
        public bool IsCleansable;
        public float EndTime;
    }
    private readonly Dictionary<object, Debuff> debuffs = new();
    private readonly List<object> toRemove = new();

    public float SpeedMultiplier { get; private set; } = 1f;
    public bool IsStunned { get; private set; }

    public void Slow(object source, float multiplier, float duration = float.PositiveInfinity, bool cleansable = true)
        => Apply(source, multiplier, false, duration, cleansable);
    public void Stun(object source, float duration = float.PositiveInfinity, bool cleansable = true)
        => Apply(source, 1f, true, duration, cleansable);
    private void Apply(object source, float multiplier, bool stuns, float duration, bool cleansable)
    {
        debuffs[source] = new Debuff()
        {
            SpeedMultiplier = Mathf.Max(0f, multiplier),
            Stuns = stuns,
            EndTime = Time.time + duration,
            IsCleansable = cleansable
        };
        Calculate();
    }
    public void Cleanse()
    {
        foreach (var pair in debuffs)
        {
            if (pair.Value.IsCleansable) toRemove.Add(pair.Key);
        }
        RemoveCollected();
    }
    private void Calculate()
    {
        SpeedMultiplier = 1f;
        IsStunned = false;
        foreach (var debuff in debuffs.Values)
        {
            SpeedMultiplier *= debuff.SpeedMultiplier;
            IsStunned |= debuff.Stuns;
        }
    }
    private void Update()
    {
        foreach (var pair in debuffs)
            if (Time.time >= pair.Value.EndTime) toRemove.Add(pair.Key);

        RemoveCollected();
    }
    private void RemoveCollected()
    {
        if (toRemove.Count == 0) return;

        foreach (var source in toRemove) debuffs.Remove(source);
        toRemove.Clear();
        Calculate();
    }
    public void Remove(object source)
    {
        if (debuffs.Remove(source)) Calculate();
    }
    private void OnDisable()
    {
        debuffs.Clear();
        toRemove.Clear();
        Calculate();
    }

}
