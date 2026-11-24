using System.Collections.Generic;
using UnityEngine;

namespace Impfany.Modifiers
{
    // Sammelt alle Buffs und Debuffs einer Figur:
    // jede Quelle (Item, Gegner, Fähigkeit ...) wird gespeichert, Get() rechnet alles zusammen.
    // Stun = Apply(quelle, Stat.MoveSpeed, 0f, dauer)
    public class StatModifiers : MonoBehaviour, IModifiable
    {
        private struct Modifier
        {
            public float Multiplier;
            public bool IsCleansable;
            public float EndTime;
        }

        // Schlüssel = (Quelle, Stat), damit eine Quelle mehrere Stats verändern kann
        private readonly Dictionary<(object source, Stat stat), Modifier> modifiers = new();
        private readonly List<(object source, Stat stat)> toRemove = new();

        public void Apply(object source, Stat stat, float multiplier, float duration = float.PositiveInfinity, bool cleansable = true)
        {
            modifiers[(source, stat)] = new Modifier()
            {
                Multiplier = Mathf.Max(0f, multiplier),
                IsCleansable = cleansable,
                EndTime = Time.time + duration
            };
        }

        public float Get(Stat stat)
        {
            float result = 1f;
            foreach (var pair in modifiers)
                if (pair.Key.stat == stat) result *= pair.Value.Multiplier;
            return result;
        }

        public void Remove(object source)
        {
            foreach (var key in modifiers.Keys)
                if (key.source == source) toRemove.Add(key);
            RemoveCollected();
        }

        // Entfernt alles mit cleansable = true (Debuffs), Items bleiben
        public void Cleanse()
        {
            foreach (var pair in modifiers)
                if (pair.Value.IsCleansable) toRemove.Add(pair.Key);
            RemoveCollected();
        }

        private void Update()
        {
            foreach (var pair in modifiers)
                if (Time.time >= pair.Value.EndTime) toRemove.Add(pair.Key);
            RemoveCollected();
        }

        private void RemoveCollected()
        {
            foreach (var key in toRemove) modifiers.Remove(key);
            toRemove.Clear();
        }
    }
}
