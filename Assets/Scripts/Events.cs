using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static Events Instance { get; private set; }

    public UnityEvent StartTimerEvent;
    public UnityEvent StartGameEvent;
    public UnityEvent WinEvent;
    public UnityEvent LoseEvent;

    public GameStartTimer gameStartTimer;

    private void Awake()
    {
        Instance = this;
        StartTimerEvent.AddListener(gameStartTimer.StartTimerMethod);
    }

    private void Start()
    {
    }
}
