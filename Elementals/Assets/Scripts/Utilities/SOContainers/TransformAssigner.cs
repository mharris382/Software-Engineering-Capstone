using UnityEngine;

public class TransformAssigner : ValueAssignerBase<Transform>
{
    public TransformContainer container;
    public override ContainerBase<Transform> GetContainer() => container;

    public override Transform GetValue() => transform;
}