using UnityEngine;

public abstract class ValueAssignerBase<T> : MonoBehaviour
{
    public abstract ContainerBase<T> GetContainer();
    public abstract T GetValue();

    private void Awake()
    {
        GetContainer().Value = GetValue();
    }
}