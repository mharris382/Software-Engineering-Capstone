using UnityEngine;

public class CasterInputHandler : MonoBehaviour
{
    private CasterState _state;

    private void Start()
    {
        _state = GetComponent<CasterState>();
    }

    private void Update()
    {
        _state.input.CastBasic  =  UnityEngine.Input.GetButtonDown("Fire1");
        _state.input.CastStrong =  UnityEngine.Input.GetButtonDown("Fire2");
        _state.input.Gathering     =  UnityEngine.Input.GetButton("Fire3");
    }
}