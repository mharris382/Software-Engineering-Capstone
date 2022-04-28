using System;
using UniRx;

namespace Elements.Totem
{
    /// <summary>
    /// interface to decouple the UI controller and the input implementation 
    /// </summary>
    public interface ITotemInputHandler
    {
        int GetElementSelectionInputAxis();

    }

    /// <summary>
    /// implementation of the null object pattern for ITotemInputHandler
    /// </summary>
    public class NullTotemInput : ITotemInputHandler
    {
        private static NullTotemInput _instance;
        public static NullTotemInput Instance => _instance ??= new NullTotemInput();
        public IObservable<int> CreateInputAxisCycleElements() => Observable.Never<int>();
        public int GetElementSelectionInputAxis() => 0;
    }
}