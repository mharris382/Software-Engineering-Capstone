using System;
using UnityEngine;

public interface IPlayerSaveLoad
{
    public Vector2 Position { get; set; }
    public float HealthPercent { get; set; }
    public float ManaPercent { get; set; }
    
    public string PlayerElement { get; set; }
}