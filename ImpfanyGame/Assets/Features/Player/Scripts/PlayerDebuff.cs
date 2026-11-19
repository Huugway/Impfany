using Impfany.Debuffs;
using System.Collections.Generic;
using UnityEngine;

public class MovementDebuff : MonoBehaviour, IMovementDebuff
{
    private struct Debuff
    {
        public float SpeedMultiplier;
        public bool Stuns;
        public bool IsCleansable;
        public float EndTime;
    }
    private readonly Dictionary<object, Debuff> allDebuffs = new();
    private readonly List<object> toRemove = new();

    public float SpeedMultiplier { get; private set; } = 1f;
    public bool IsStunned { get; private set; }

    public void Slow(object source, float movementMultiplier, float duration = float.PositiveInfinity, bool isCleansable = true)
        => Apply(source, movementMultiplier, false, duration, isCleansable);
    public void Stun(object source, float duration = float.PositiveInfinity, bool isCleansable = true)
        => Apply(source, 1f, true, duration, isCleansable);
    private void Apply(object source, float multiplier, bool stuns, float duration, bool cleansable)
    {
        allDebuffs[source] = new Debuff()
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
        foreach (var pair in allDebuffs)
        {
            if (pair.Value.IsCleansable) toRemove.Add(pair.Key);
        }
        RemoveCollected();
    }
    private void Calculate()
    {
        SpeedMultiplier = 1f;
        IsStunned = false;
        foreach (var debuff in allDebuffs.Values)
        {
            SpeedMultiplier *= debuff.SpeedMultiplier;
            IsStunned |= debuff.Stuns;
        }
    }
    private void Update()
    {
        foreach (var pair in allDebuffs)
            if (Time.time >= pair.Value.EndTime) toRemove.Add(pair.Key);

        RemoveCollected();
    }
    private void RemoveCollected()
    {
        if (toRemove.Count == 0) return;

        foreach (var source in toRemove) allDebuffs.Remove(source);
        toRemove.Clear();
        Calculate();
    }
    public void Remove(object source)
    {
        if (allDebuffs.Remove(source)) Calculate();
    }
    private void OnDisable()
    {
        allDebuffs.Clear();
        toRemove.Clear();
        Calculate();
    }

}
