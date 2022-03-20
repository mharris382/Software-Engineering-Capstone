using UnityEngine;

[CreateAssetMenu(menuName="Containers/Save Path")]
public class SavePathContainer : ContainerBase<string>
{
    public string _string;
    protected override string GetValue() => HasValueAssigned()  ? $"{Application.persistentDataPath}/{_string}/" : _string;
    protected override bool HasValueAssigned() => !string.IsNullOrEmpty(_string);
    protected override void SetValue(string value) => _string = value;
}