public interface IHealth
{
    bool isAlive { get; }
    void healHealth(float amount);
    void damageHealth(float amount);

    public Element Element { get; }
}