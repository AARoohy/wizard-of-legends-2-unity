using System;
using System.Collections.Generic;
using System.Linq;
using StatSystem;
using UnityEngine;

namespace StatSystem
{
    public abstract class Stat
    {
        public abstract StatType Type { get; }

        public virtual float Value
        {
            get => _value;
            private set
            {
                var old = _value;
                _value = value;
                OnValueChanged?.Invoke(old, value);
            }
        }

        protected List<StatModifier> _modifiers;

        protected float _value;

        public Action<float, float> OnValueChanged;

        public Stat()
        {
            _modifiers = new List<StatModifier>();
        }

        public Stat(int baseValue)
        {
            _modifiers = new List<StatModifier>();
            _modifiers.Add(new StatModifier(Type, baseValue));
        }


        public void AddModifier(StatModifier statModifier)
        {
            _modifiers.Add(statModifier);
            Value = UpdateValue();
        }

        protected virtual float UpdateValue()
        {
            var a = 0f;
            _modifiers = _modifiers.OrderBy(a => a.StatType).ToList();
            float percentage = 1;
            foreach (var modif in _modifiers)
                switch (modif.Type)
                {
                    case StatModifier.Types.Base:
                        a = modif.Value;
                        break;
                    case StatModifier.Types.Direct:
                        a += modif.Value;
                        break;
                    case StatModifier.Types.Percentage:
                        percentage += (modif.Value / 100f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            a *= percentage;
            return a;
        }

        public static implicit operator float(Stat stat)
        {
            return stat.Value;
        }

        public static implicit operator int(Stat stat)
        {
            return (int)stat.Value;
        }
    }
}

public class MoveSpeedStat : Stat
{
    public override StatType Type => StatType.MoveSpeed;

    public MoveSpeedStat() : base() { }

    public MoveSpeedStat(int baseValue) : base(baseValue) { }
    protected override float UpdateValue()
    {
        return base.UpdateValue();
    }
}

public class HealthStat : Stat
{
    public override StatType Type => StatType.Health;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => Value;

    public HealthStat() : base() 
    { 
        CurrentHealth = 0; 
    }

    public HealthStat(int baseValue) : base(baseValue) 
    {
        CurrentHealth = baseValue;
    }

    protected override float UpdateValue()
    {
        var oldMaxHealth = Value;
        var newValue = base.UpdateValue(); 
        var healthPercentage = CurrentHealth / oldMaxHealth;
        CurrentHealth = newValue * healthPercentage; 
        return newValue;
    }
    
    public void Heal(float amount)
    {
        CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
        OnValueChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Math.Max(CurrentHealth - amount, 0);
        OnValueChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}

public class AttackPowerStat : Stat
{
    public override StatType Type => StatType.AttackPower;

    public AttackPowerStat() : base() { }

    public AttackPowerStat(int baseValue) : base(baseValue) { }
}

public class DefenseStat : Stat
{
    public override StatType Type => StatType.Defense;

    public DefenseStat() : base() { }

    public DefenseStat(int baseValue) : base(baseValue) { }
}

public class ManaStat : Stat
{
    public override StatType Type => StatType.Mana;

    public ManaStat() : base() { }

    public ManaStat(int baseValue) : base(baseValue) { }
}

public class StaminaStat : Stat
{
    public override StatType Type => StatType.Stamina;

    public StaminaStat() : base() { }

    public StaminaStat(int baseValue) : base(baseValue) { }
}

public class CritChanceStat : Stat
{
    public override StatType Type => StatType.CritChance;

    public CritChanceStat() : base() { }

    public CritChanceStat(int baseValue) : base(baseValue) { }
}

public class CritDamageStat : Stat
{
    public override StatType Type => StatType.CritDamage;

    public CritDamageStat() : base() { }

    public CritDamageStat(int baseValue) : base(baseValue) { }
}

public class AttackSpeedStat : Stat
{
    public override StatType Type => StatType.AttackSpeed;

    public AttackSpeedStat() : base() { }

    public AttackSpeedStat(int baseValue) : base(baseValue) { }
}
