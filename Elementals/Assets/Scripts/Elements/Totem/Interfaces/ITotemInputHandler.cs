namespace Elements.Totem
{
    /// <summary>
    /// interface to decouple the UI controller and the input implementation 
    /// </summary>
    public interface ITotemInputHandler
    {
        
    }

    /// <summary>
    /// implementation of the null object pattern for ITotemInputHandler
    /// </summary>
    public class NullTotemInput : ITotemInputHandler
    {
        private static NullTotemInput _instance;
        public static NullTotemInput Instance => _instance ??= new NullTotemInput();
    }
}