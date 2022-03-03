using System;

public interface IHealth
{
    bool isAlive { get; }
    void healHealth(float amount);
    void damageHealth(float amount);
}

public class HealthState : StatusValue, IHealth
{
    public bool isAlive => CurrentValue > 0;

    public void healHealth(float amount)
    {
        CurrentValue += amount;
    }

    public void damageHealth(float amount)
    {
        CurrentValue -= amount;
    }
}