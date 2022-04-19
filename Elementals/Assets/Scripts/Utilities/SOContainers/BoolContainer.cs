using UnityEngine;

[CreateAssetMenu(menuName="Containers/Bool Container")]
public class BoolContainer : ContainerBase<bool>
{
    [SerializeField]
    private bool boolValue;
    protected override bool GetValue() => boolValue;

    protected override bool HasValueAssigned() => true;

    protected override void SetValue(bool value) => boolValue = value;
}