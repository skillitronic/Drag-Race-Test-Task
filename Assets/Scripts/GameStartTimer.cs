using System.Collections;
using UnityEngine;

public class GameStartTimer : MonoBehaviour
{

    public UIController UIController;
    private void OnEnable()
    {
        Events.Instance.StartTimerEvent.AddListener(StartTimerMethod);
    }

    public void StartTimerMethod()
    {
        StartCoroutine("StartTimer");
    }

    public IEnumerator StartTimer()
    {
        float timer = 3;
        while (timer > 0)
        {
            UIController.timerText.SetText(timer.ToString());
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if (timer == 0)
        {
            UIController.timerText.SetText("GO");
            yield return new WaitForSeconds(1f);
        }

        Events.Instance.StartGameEvent.Invoke();
    }
}
