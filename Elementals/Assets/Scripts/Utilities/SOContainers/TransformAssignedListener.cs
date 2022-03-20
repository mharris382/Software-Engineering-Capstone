using UnityEngine;
using UnityEngine.Events;

public class TransformAssignedListener : ValueAssignedListener<Transform>
{
    public TransformContainer container;
    public UnityEvent<Transform> onAssigned;
    public override ContainerBase<Transform> GetContainer() => container;

    public override void OnValueAssigned(Transform value) => onAssigned?.Invoke(value);
}