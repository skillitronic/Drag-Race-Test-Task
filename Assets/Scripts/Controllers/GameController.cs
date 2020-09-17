using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public List<Level> levelList = null;

    public Transform instantiater;

    public int score;

    public int clicksToWin;


    private void Awake()
    {
        Instance = this;
        SaveSystem.Load("levels", ref SaveData.Current.levelIndex);
    }

    private void OnEnable()
    {
        Events.Instance.GreenZoneClickEvent += () => IncreaseScore(500);
        Events.Instance.BlueZoneClickEvent += () => IncreaseScore(1500);

        Events.Instance.WinEvent.AddListener(() => levelList[SaveData.Current.levelIndex].isWon = true);
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
        } else if (levelList[SaveData.Current.levelIndex].isWon == true && levelList[SaveData.Current.levelIndex].isChosen == false)
        {
            SceneController.AddSceneByName("UpgradeScene");
        }
    }

}
