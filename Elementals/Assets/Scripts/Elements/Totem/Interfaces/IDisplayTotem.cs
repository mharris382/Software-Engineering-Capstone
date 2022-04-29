namespace Elements.Totem
{
    public interface ITotemDisplay
    {
        
    }
    public interface IDisplayTotemElement : ITotemDisplay
    {
        public Element Element { set; }
    }
    public interface IDisplayTotemRadius: ITotemDisplay
    {
        public float Radius { set; }
    }
    public interface IDisplayTotemColor: ITotemDisplay
    {
        public UnityEngine.Color Color { set; }
    }
    public interface IDisplayTotemChargeState: ITotemDisplay
    {
        public bool IsCharging { set; }
    }
}