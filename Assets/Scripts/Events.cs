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

    private GameStartTimer gameStartTimer;

    private void Awake()
    {
        Instance = this;
        gameStartTimer = GetComponent<GameStartTimer>();
    }

    private void Start()
    {
        StartTimerEvent.AddListener(gameStartTimer.StartTimerMethod);
    }
}
