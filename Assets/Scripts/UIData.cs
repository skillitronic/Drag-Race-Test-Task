using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIData 
{
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI winnerText;

    public Slider bar1;
    public Slider bar2;


    public void UpdateTimer(string timerValue)
    {
        timerText.SetText(timerValue);
    }

    public void TurnCanvas(Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }

}

