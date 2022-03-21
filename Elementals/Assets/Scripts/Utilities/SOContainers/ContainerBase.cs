using System;
using System.Collections.Generic;
#if UNIRX
using UniRx;
#endif
using UnityEngine;
using UnityEngine.Events;

public abstract class ContainerBase<T> : ScriptableObject
{
    public T Value
    {
        get => GetValue();
        set
        {
            T v = GetValue();
            
            if (!HasValueAssigned() || !v.Equals(value))
            {
                SetValue(value);
                foreach (var listener in valueAssignedListeners)
                {
                    if(listener!=null)listener?.Invoke(value);
                }
            }
        }
    }

    protected abstract T GetValue();
    protected abstract bool HasValueAssigned();
    protected abstract void SetValue(T value);
    private List<Action<T>> valueAssignedListeners = new List<Action<T>>();


    public IDisposable AddListener(Action<T> listener)
    {
        valueAssignedListeners.Add(listener);
        if (Value != null) listener?.Invoke(GetValue());
        return Disposable.Create(() => valueAssignedListeners.Remove(listener));
    }
}

#if !UNIRX
public static class Disposable
{
    public static IDisposable Create(Action onDispose) => new AD(onDispose);

    private class AD : IDisposable
    {
        private Action _onD;

        public AD(Action onD)
        {
            this._onD = onD;
        }
        public void Dispose()
        {
            _onD?.Invoke();
        }
    }
}
#endif 



