using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public List<Level> levelList;
    public UIData UIData;
    public int score;
    public Transform instantiater;

    //Event StartTimer
    public UnityEvent StartTimerEvent;

    //Event StartGame
    public UnityEvent StartGameEvent;
    //Event WinGame
    public UnityEvent WinGameEvent;


    //Event LoseGame

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartTimerEvent.AddListener(StartTimerMethod);
    }

    private void OnDisable()
    {

    }

    private void Start()
    {

        SaveSystem.Load("levels", ref SaveData.Current.levelIndex);
        Debug.Log(SaveData.Current.levelIndex);

        if (levelList[SaveData.Current.levelIndex].isWon == false)
        {
            Instantiate(levelList[SaveData.Current.levelIndex].levelReference, instantiater);
        }
        StartTimerEvent.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void StartTimerMethod()
    {
        StartCoroutine(nameof(StartTimer));
    }

    private IEnumerator StartTimer()
    {
        float timer = 3;
        while (timer > 0)
        {
            UIData.UpdateTimer(timer.ToString());
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if (timer == 0)
        {
            UIData.timerText.SetText("GO");
            yield return new WaitForSeconds(1f);
        }

        StartGameEvent.Invoke();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private void ActivateBars()
    {
        // activate bar 1 logic
        // activate bar 2 logic
    }

    //bar 1 logic
    //bar 2 logic
}
