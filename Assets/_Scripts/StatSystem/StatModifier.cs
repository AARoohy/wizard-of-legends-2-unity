

[System.Serializable]
public class StatModifier
{
    public StatType StatType;
    public Types Type;
    public float Value;

    public StatModifier(StatType type, float value)
    {
        StatType = type;
        Value = value;
    }
    
    public enum Types
    {
        Base,
        Direct,
        Percentage
    }
}