using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEventSO _event;
    public UnityEvent response;

    private void OnEnable()
    {
        _event.AddListener(this);
    }

    private void OnDisable()
    {
        _event.RemoveListener(this);
    }
    public void OnEventRaised()
    {
        response.Invoke();  
    }
}
