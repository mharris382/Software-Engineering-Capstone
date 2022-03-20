
using System;
using UnityEngine;

public abstract class ValueAssignedListener<T> : MonoBehaviour
{
    public abstract ContainerBase<T> GetContainer();
    public abstract void OnValueAssigned(T value);

    private IDisposable _disposable;

    private void Awake()
    {
        _disposable = GetContainer().AddListener(OnValueAssigned);
        Awake1();
    }

    protected virtual void Awake1() { }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
    
}