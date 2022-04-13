using System.Collections;
using UnityEngine;

public class AsyncExample : MonoBehaviour
{
    public bool runAsCoroutine;
    public void Start()
    {
        if (runAsCoroutine)
        {
            StartCoroutine(Coroutine());
        }
        else
        {
            Print();
            Print();
        }
    }

    void Print()
    {
        Debug.Log($"<b>{name}</b>: Frames passed {Time.frameCount}");
    }

    IEnumerator Coroutine()
    {
        Print();
        yield return null;
        Print();
        yield return new WaitForSeconds(1);
        Print();
    }
}