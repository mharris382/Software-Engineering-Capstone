using System.Collections.Generic;

public interface IModifySpeed
{
    public IEnumerable<SpeedModifier> GetSpeedModifiers();
}