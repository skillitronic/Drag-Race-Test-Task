using TMPro;
using UnityEngine;
using DG.Tweening;
using KoganeUnityLib;
using System.Collections;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private float fadeInTimer = 0;
    [SerializeField] private float fadeOutTimer = 0;

    public TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI winnerText = null;
    [SerializeField] private TextMeshProUGUI loseText = null;
    [SerializeField] private TMP_Typewriter winnerTextAnimation = null;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Instance = this;
        Events.Instance.WinEvent.AddListener(WinText);
        Events.Instance.LoseEvent.AddListener(LoseText);        
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

    public void WinText()
    {
        //winnerTextAnimation.gameObject.SetActive(true);
        winnerText.alpha = 1f;
        scoreText.alpha = 0f;
        winnerTextAnimation.Play
            (
            text: "WINNER",
            speed: 5f,
            onComplete: () =>
                {
                    winnerText.DOFade(0, .5f).SetDelay(.5f).OnComplete(() => /*winnerText.gameObject.SetActive(false)*/
                    StartCoroutine("ScoreAnimation"));
                }
            );
    }

    public void LoseText()
    {
        loseText.DOFade(0, 3f).SetDelay(1f).OnComplete(SceneController.RestartScene);
    }

    private IEnumerator ScoreAnimation()
    {
        yield return new WaitForSeconds(1f);
        scoreText.alpha = 1f;
        float number = 0;
        while (number < GameController.Instance.score)
        {
            number += 25;
            scoreText.SetText(number.ToString());
            yield return new WaitForSeconds(.02f);
        }
        scoreText.DOFade(0, 1f).OnComplete(() =>
        {
            Events.Instance.UpgradeEvent?.Invoke();

        }).SetDelay(1f);
    }
}