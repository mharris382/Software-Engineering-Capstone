using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class VictoryTrigger : MonoBehaviour
{
    public bool isFinalLevel => level == 3;
    [Range(1,3)]
    public int level = 1;

    private bool _reachedVictory = true;
    public float delay = 0.5f;
    public void OnVictory()
    {
        _reachedVictory = false;
        SceneManager.LoadScene(level + 1);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_reachedVictory)
        {
            return;
        }
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        if (rb.gameObject.CompareTag("Player"))
        {
            _reachedVictory = true;
            Invoke("OnVictory", delay);
        }
    }
}
