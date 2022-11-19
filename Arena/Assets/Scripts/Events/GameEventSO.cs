using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Events/New Event")]
public class GameEventSO : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void AddListener(GameEventListener l)
    {
        if (!eventListeners.Contains(l))
            eventListeners.Add(l);
    }

    public void RemoveListener(GameEventListener l)
    {
        if (eventListeners.Contains(l))
            eventListeners.Remove(l);
    }
}
