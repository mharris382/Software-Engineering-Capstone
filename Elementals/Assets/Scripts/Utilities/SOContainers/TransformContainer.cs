using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName="Containers/Transform")]
public class TransformContainer : ContainerBase<Transform>
{
    public Transform transform;

    protected override Transform GetValue() => transform;
    protected override bool HasValueAssigned()
    {
        return transform != null;
    }

    protected override void SetValue(Transform value) => transform = value;
}