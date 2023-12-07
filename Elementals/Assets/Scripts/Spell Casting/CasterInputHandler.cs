using UnityEditor;
using UnityEngine;

public class CasterInputHandler : MonoBehaviour
{
    private CasterState _state;
    public bool allowCasting = true;
    private void Start()
    {
        _state = GetComponent<CasterState>();
    }

    private void Update()
    {
        // if (allowCasting)
        // {
            _state.input.IsCastBasicDown  =  Input.GetButtonDown("Fire1");
            _state.input.IsCastStrongDown =  Input.GetButtonDown("Fire2");
            _state.input.IsCastBasicHeld  =  Input.GetButton("Fire1");
            _state.input.IsCastStrongHeld =  Input.GetButton("Fire2");
            _state.input.IsGatheringHeld     =  Input.GetButton("Fire3");
        // }

    }
}