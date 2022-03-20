using UnityEngine;

[CreateAssetMenu(menuName="Containers/String")]
public class StringContainer : ContainerBase<string>
{
    public string _string;
    protected override string GetValue() => _string;
    protected override bool HasValueAssigned() => !string.IsNullOrEmpty(_string);
    protected override void SetValue(string value) => _string = value;
}