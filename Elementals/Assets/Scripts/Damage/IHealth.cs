using Damage;

public interface IHealth
{
    bool isAlive { get; }
    void healHealth(float amount);

    void damageHealth(float amount);
    void damageHealth(DamageInfo damageInfo);

    public Element Element { get; }
}


