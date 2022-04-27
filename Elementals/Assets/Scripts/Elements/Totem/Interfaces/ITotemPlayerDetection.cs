namespace Elements.Totem
{
    /// <summary>
    /// implements the strategy for determining if the player is inside or outside the totem range of influence
    /// </summary>
    public interface ITotemPlayerDetection
    {
        
    }

    /// <summary>
    /// implementation of the null object pattern for ITotemInputHandler
    /// </summary>
    public class NullTotemPlayerDetection : ITotemPlayerDetection
    {
        private static NullTotemPlayerDetection _instance;
        public static NullTotemPlayerDetection Instance => _instance ??= new NullTotemPlayerDetection();
    }
}