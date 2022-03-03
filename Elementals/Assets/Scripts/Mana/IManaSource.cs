public interface IManaSource
{
    public float CurrentValue { get; }
    public float MaxValue { get; }
    public bool HasMana(float amount);
    public void AddMana(float amount);
    public void RemoveMana(float amount);
}