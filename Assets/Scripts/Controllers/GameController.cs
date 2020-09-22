using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public List<Level> levelList = null;

    [Space(10f)]
    public EquipedUpgrades equipedUpgrades = null;

    public Transform instantiater;

    public int score;

    [HideInInspector] public Transform carReference;
    [HideInInspector] public Transform finishLineReference;


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
        Events.Instance.UpgradeEvent += CheckForUpgrades;
    }

    private void OnDisable()
    {
        Events.Instance.GreenZoneClickEvent -= () => IncreaseScore(500);
        Events.Instance.BlueZoneClickEvent -= () => IncreaseScore(1500);

        Events.Instance.WinEvent.RemoveListener(() => levelList[SaveData.Current.levelIndex].isWon = true);
        Events.Instance.UpgradeEvent -= CheckForUpgrades;

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

    private void CheckForUpgrades()
    {
        if (!levelList[SaveData.Current.levelIndex].upgrades.Any())
        {
            score = 0;
            levelList[SaveData.Current.levelIndex].isChosen = true;
            SaveData.Current.levelIndex += 1;
            SaveSystem.Save("levels", SaveData.Current.levelIndex);
            DestroyLevel();
            SpawnLevel();

        }
        else
        {
            DestroyLevel();
            SceneController.AddSceneByName("UpgradeScene");
        }
    }
}
