using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// triggers the progression from one level to the next
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class VictoryTrigger : MonoBehaviour
{
    public bool isFinalLevel => level == 3;
    public bool isTutorialLevel;
    [Range(1,3)]
    public int level = 1;

    private bool _reachedVictory = true;
    public float delay = 0.5f;
    
    public void OnVictory()
    {
        if (isTutorialLevel) SceneManager.LoadScene(1);
        else SceneManager.LoadScene(level + 1);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        if (rb.gameObject.CompareTag("Player"))
        {
            Invoke("OnVictory", delay);
        }
    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene(level + 1);
    }
}