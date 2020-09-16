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

    public UnityAction restartTimerEvent;
    public UnityAction ZoneClickEvent;
    public UnityAction GreenZoneClickEvent;
    public UnityAction BlueZoneClickEvent;


    private void Awake()
    {
        Instance = this;
    }


}