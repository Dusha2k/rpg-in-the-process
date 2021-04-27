using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;
    
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    public void OnEnable()
    {
        signal.RegisterListeners(this);
    }

    public void OnDisable()
    {
        signal.DeRegisterListeners(this);
    }
}
