using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public List<Level> levelList;
    public Camera gameCamera;
    public Transform instantiater;



    public int score;

    public int clicksToWin;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
