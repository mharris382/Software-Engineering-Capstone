using System.Collections.Generic;

public interface ISpeedModifyingObject
{
    public IEnumerable<SpeedModifier> GetSpeedModifiers();
}
