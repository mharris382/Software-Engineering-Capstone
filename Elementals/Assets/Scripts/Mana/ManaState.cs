using System;

public class ManaState : StatusValue, IManaSource
{
    public bool HasMana(float amount)
    {
        return CurrentValue > amount;
    }

    public void AddMana(float amount)
    {
        CurrentValue += amount;
    }

    public void RemoveMana(float amount)
    {
        CurrentValue -= amount;
    }
}