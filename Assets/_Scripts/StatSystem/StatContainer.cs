using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StatSystem;

public class StatContainer: MonoBehaviour
{
    private Dictionary<StatType, Stat> _stats;

    public void Setup()
    {
        _stats = new Dictionary<StatType, Stat>();
        var stats = new List<Stat>()
        {
            new MoveSpeedStat(),
            new HealthStat(),
            new AttackPowerStat(),
            new DefenseStat(),
            new ManaStat(),
            new StaminaStat(),
            new CritChanceStat(),
            new CritDamageStat(),
            new AttackSpeedStat()
        };
        foreach (var stat in stats)
        {
            if (_stats.ContainsKey(stat.Type))
            {
                Debug.Log($"Multiple Stat Initialization Error: Type {stat.Type}");
                continue;
            }
            _stats.Add(stat.Type,stat);
        }
    }
  

    public void AddModifier(object modifier, params StatModifier[] statModifiers)
    {
        foreach (var mod in statModifiers)
            _stats[mod.StatType].AddModifier(mod);
    }

    public Stat GetStat(StatType type) => _stats[type];

    public T GetStat<T>() where T : Stat
    {
        return (T)_stats.FirstOrDefault(a => a.Value.GetType() == typeof(T)).Value;
    }
}