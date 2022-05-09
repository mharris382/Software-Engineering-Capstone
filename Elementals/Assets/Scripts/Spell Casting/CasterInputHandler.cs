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
            _state.input.CastBasic  =  Input.GetButtonDown("Fire1");
            _state.input.CastStrong =  Input.GetButtonDown("Fire2");
            _state.input.Gathering     =  Input.GetButton("Fire3");
        // }

    }
}