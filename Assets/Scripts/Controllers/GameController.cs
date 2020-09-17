using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private List<Level> levelList = null;

    public Transform instantiater;

    public int score;

    public int clicksToWin;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Events.Instance.GreenZoneClickEvent += () => IncreaseScore(500);
        Events.Instance.BlueZoneClickEvent += () => IncreaseScore(1500);
    }

    private void OnDisable()
    {
        Events.Instance.GreenZoneClickEvent -= () => IncreaseScore(500);
        Events.Instance.BlueZoneClickEvent -= () => IncreaseScore(1500);
    }

    private void Start()
    {
        SpawnLevel();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private void SpawnLevel()
    {
        if (levelList[SaveData.Current.levelIndex].isWon == false)
        {
            Instantiate(levelList[SaveData.Current.levelIndex].levelReference, instantiater);
            Events.Instance.StartTimerEvent.Invoke();
        }
    }

}
