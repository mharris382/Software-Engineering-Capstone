using System;
using UnityEngine;

public class PlayerSaveLoader : MonoBehaviour, IPlayerSaveLoad
{
    private const Element PlayerDefaultElement = Element.Water;
    [SerializeField] 
    private ElementContainer elementContainer;
    private ManaState _mana;
    private HealthState _health;
    
    private void Awake()
    {
        _mana = GetComponentInChildren<ManaState>();
        _health = GetComponentInChildren<HealthState>();
    }

    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public float HealthPercent
    {
        get => _health.CurrentValue / _health.MaxValue;
        set => _health.CurrentValue = _health.MaxValue * value;
    }

    public float ManaPercent
    {
        get =>  _mana.CurrentValue / _mana.MaxValue;
        set =>  _mana.CurrentValue = _mana.MaxValue * value;
    }

    public string PlayerElement
    {
        get => elementContainer.Element.ToString();
        set
        {
            try
            {
                elementContainer.Element = (Element) Enum.Parse(typeof(Element), value);
            }
            catch
            {
                elementContainer.Element = PlayerDefaultElement;
            }
        }
    }
}