using TMPro;
using UnityEngine;
using DG.Tweening;
using KoganeUnityLib;
using System.Collections;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private float fadeInTimer;
    [SerializeField] private float fadeOutTimer;

    public TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TMP_Typewriter winnerTextAnimation;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Events.Instance.WinEvent.AddListener(WinShit);
    }

    private void OnDisable()
    {
        Events.Instance.WinEvent.RemoveListener(WinShit);
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
        winnerTextAnimation.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

//You need to add 1 more character to not broke the animation
        winnerTextAnimation.Play
            (
            text: "WINNERr",
            speed: 5f,
            onComplete: () =>
                {
                    winnerTextAnimation.gameObject.SetActive(false);
                    scoreText.gameObject.SetActive(true);
                    StartCoroutine(nameof(ScoreAnimation));
                }
            );
    }

    private IEnumerator ScoreAnimation()
    {
        float number = 0;
        while (true)
        {
            if (number < GameController.Instance.score)
            {

                number += 25;
                scoreText.SetText(number.ToString());
                yield return new WaitForSeconds(.05f);
            }
            yield return new WaitForSeconds(1f);
            scoreText.DOFade(0, 1f).OnComplete(() => scoreText.gameObject.SetActive(false));
        }
    }
}