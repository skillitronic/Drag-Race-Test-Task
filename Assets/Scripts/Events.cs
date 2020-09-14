using System;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static Events Instance { get; private set; }

    public UnityEvent StartTimerEvent;
    public UnityEvent StartGameEvent;
    public UnityEvent WinEvent;
    public UnityEvent LoseEvent;

    public Action restartTimer;


    private void Awake()
    {
        Instance = this;
    }

}
