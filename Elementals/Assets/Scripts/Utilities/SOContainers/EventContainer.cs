using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Containers/Events/New Event")]
public class EventContainer : ScriptableObject
{
    [SerializeField] private UnityEvent onEvent;

    public UnityEvent Event => onEvent;
    public void Invoke()
    {
        onEvent?.Invoke();    
    }
}

public abstract class EventContainer<T> : ScriptableObject
{
    [SerializeField]
    private UnityEvent<T> onEvent;



    public void Invoke(T value)
    {
        onEvent?.Invoke(value);
    }
}

