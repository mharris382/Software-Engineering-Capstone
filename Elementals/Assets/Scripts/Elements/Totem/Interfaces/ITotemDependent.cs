namespace Elements.Totem
{
    public interface ITotemDependent
    {
        void InjectSharedTotemState(object tbd);
    }
}