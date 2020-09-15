using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private float fadeInTimer;
    [SerializeField] private float fadeOutTimer;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    private void Awake()
    {
        Instance = this;
    }

    public void TurnCanvas(Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }

    public void FadeInCanvas(GameObject gameObject)
    {
        gameObject.SetActive(true);

        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        gameObject.GetComponent<CanvasGroup>().DOFade(1f, fadeInTimer);
    }

    public void FadeOutCanvas(GameObject gameObject)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        gameObject.GetComponent<CanvasGroup>().DOFade(0, fadeOutTimer).OnComplete(() => gameObject.SetActive(false));
    }

    public void WinShit()
    {
        //winnerText.
    }

}