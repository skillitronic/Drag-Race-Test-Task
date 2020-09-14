using System.Collections;
using UnityEngine;

public class GameStartTimer : MonoBehaviour
{
    public void StartTimerMethod()
    {
        StartCoroutine(nameof(StartTimer));
    }

    private IEnumerator StartTimer()
    {
        Debug.Log("Started");
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
