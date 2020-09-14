using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public List<Level> levelList;
    public int score;

    public Transform instantiater;

    private void Awake()
    {
        Instance = this;

        if (levelList[SaveData.Current.levelIndex].isWon == false)
        {
            Instantiate(levelList[SaveData.Current.levelIndex].levelReference, instantiater);
            Events.Instance.StartTimerEvent.Invoke();
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

}
