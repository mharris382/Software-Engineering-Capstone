using System;
using UnityEngine;

public class TotemController : MonoBehaviour
{
}

public struct TotemInfo
{
    private float _charge;
    public float Charge
    {
        get => _charge;
        set
        {
            var v = Mathf.Clamp01(value);
            if (Math.Abs(v - _charge) > 0.0001f)
            {
                _charge = v;
            }
        }
    }
    
    
}



[CreateAssetMenu(menuName = "Containers/State/TotemState")]
public class TotemStateContainer : ScriptableObject
{
    [System.Serializable]
    public class Config
    {
        [Min(5), Tooltip("How long does it take for totem to be used again")]
        public float rechargeTime = 60;
    }
}

public interface ITotemListener
{
    public void SetTotemState(TotemInfo totemInfo)
    {
    }
}