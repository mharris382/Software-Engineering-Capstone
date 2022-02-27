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
        _state.Casting       = _state.AllowCast && Input.GetButtonDown("Fire1");
        _state.CastingStrong = _state.AllowCast && Input.GetButtonDown("Fire2");
        _state.Gathering     = _state.AllowCast && Input.GetButton("Fire3");
    }
}