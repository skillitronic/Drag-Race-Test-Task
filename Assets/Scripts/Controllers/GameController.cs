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
        Events.Instance.LoseEvent.AddListener(SceneController.RestartScene);

        Events.Instance.UpgradeEvent += () => SceneController.AddSceneByName("UpgradeScene");
        Events.Instance.UpgradeEvent += DestroyLevel;
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

    public void SpawnLevel()
    {
        if (levelList[SaveData.Current.levelIndex].isWon == false)
        {
            Instantiate(levelList[SaveData.Current.levelIndex].levelReference, instantiater);
            Events.Instance.StartTimerEvent.Invoke();
        }
        else if (levelList[SaveData.Current.levelIndex].isWon == true && levelList[SaveData.Current.levelIndex].isChosen == false)
        {
            SceneController.AddSceneByName("UpgradeScene");
        }
        else if (levelList[SaveData.Current.levelIndex].isWon == true && levelList[SaveData.Current.levelIndex].isChosen == true)
        {
            levelList[SaveData.Current.levelIndex].isChosen = false;
            levelList[SaveData.Current.levelIndex].isWon = false;
            Instantiate(levelList[SaveData.Current.levelIndex].levelReference, instantiater);
            Events.Instance.StartTimerEvent.Invoke();
        }
    }

    private void DestroyLevel()
    {
        for (int i = 0; i < instantiater.childCount; i++)
        {
            Destroy(instantiater.GetChild(i).gameObject);
        }
    }

}
