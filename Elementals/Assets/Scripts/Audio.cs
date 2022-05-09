using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("replaced this with AudioEvent system")]
public class Audio : MonoBehaviour
{
    [SerializeField]
    private AudioClip regularAttack;
    [SerializeField]
    private AudioClip strongAttack;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            _audioSource.clip = regularAttack;
            _audioSource.time = 0.1f;
            _audioSource.Play();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _audioSource.clip = strongAttack;
            _audioSource.Play();
        }
    }
}
