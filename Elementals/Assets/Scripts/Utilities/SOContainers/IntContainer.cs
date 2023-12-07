using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName="Containers/Int Container")]
public class IntContainer : ContainerBase<int>
{
    [FormerlySerializedAs("boolValue")] [SerializeField]
    private int intValue;
    protected override int GetValue() => intValue;

    protected override bool HasValueAssigned() => true;

    protected override void SetValue(int value) => intValue = value;
}