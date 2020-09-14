using System.Collections;
using UnityEngine;

public class GameStartTimer : MonoBehaviour
{
    private void Start()
    {
        Events.Instance.StartTimerEvent.AddListener(StartTimerMethod);
    }

    private void OnDisable()
    {
        Events.Instance.StartTimerEvent.RemoveListener(StartTimerMethod);
    }

    public void StartTimerMethod()
    {
        StartCoroutine(nameof(StartTimer));
    }

    private IEnumerator StartTimer()
    {
        float timer = 3;
        while (timer > 0)
        {
            UIController.Instance.timerText.SetText(timer.ToString());
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if (timer == 0)
        {
            UIController.Instance.timerText.SetText("GO");
            yield return new WaitForSeconds(1f);
        }

        Events.Instance.StartGameEvent.Invoke();
    }
}
