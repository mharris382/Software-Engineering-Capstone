using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialSignTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent onPlayerEnteredTrigger;
    [SerializeField] private float exitDelay = 1;
    [SerializeField] private UnityEvent onPlayerExitedTrigger;
    
    private Coroutine _exitDelayCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoExit();
        }
    }

    void DoEnter()
    {
        onPlayerEnteredTrigger?.Invoke();
        
        if (_exitDelayCoroutine != null)
        {
            StopCoroutine(_exitDelayCoroutine);
        }
    }
    void DoExit()
    {
        _exitDelayCoroutine = StartCoroutine(DoExitDelay(exitDelay));
    }
    IEnumerator DoExitDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        onPlayerExitedTrigger?.Invoke();
    }

    
}
