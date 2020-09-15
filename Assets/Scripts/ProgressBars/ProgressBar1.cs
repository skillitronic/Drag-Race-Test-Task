using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar1 : MonoBehaviour
{
    public RectTransform zonesFolder;
    public RectTransform greenZone;
    public RectTransform blueZone;
    public float timeToReach;
    private Tween tween;

    public float sliderHeight;
    public float halfSliderHeight;
    public float halfZoneHeight;
    public float zonePosition;
    public float centerZonePosition;
    public float greenRangePos;
    public float greenRangeNeg;
    public float blueRangePos;
    public float blueRangeNeg;
    public float possibleRandomRange;

    public float halfGreenZone;
    public float halfBlueZone;

    private int localClickCounter = 0;

    private void Awake()
    {
        halfGreenZone = greenZone.rect.height / 2;
        halfBlueZone = blueZone.rect.height / 2;


        sliderHeight = gameObject.GetComponent<RectTransform>().rect.height;
        halfSliderHeight = gameObject.GetComponent<RectTransform>().rect.height / 2;
        halfZoneHeight = zonesFolder.rect.height / 2;

        possibleRandomRange = halfSliderHeight - halfZoneHeight;
        zonePosition = Random.Range(-possibleRandomRange, possibleRandomRange);
        zonesFolder.localPosition = new Vector2(0, zonePosition);

        CalculateTouchZone();



        StartLoop();
    }

    private void OnEnable()
    {
        Events.Instance.LoseEvent.AddListener(StopLoop);
        localClickCounter = 0;
    }

    private void OnDisable()
    {
        Events.Instance.LoseEvent.AddListener(StopLoop);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.GetComponent<Slider>().value >= greenRangeNeg && gameObject.GetComponent<Slider>().value <= greenRangePos)
            {
                localClickCounter++;
                GameController.Instance.IncreaseScore(500);
                StopLoop();
                MoveZone();
                CalculateTouchZone();
                Events.Instance.restartTimer?.Invoke();
                //Effect
            }
            else if (gameObject.GetComponent<Slider>().value >= blueRangeNeg && gameObject.GetComponent<Slider>().value <= blueRangePos)
            {
                localClickCounter++;
                GameController.Instance.IncreaseScore(1500);
                StopLoop();
                MoveZone();
                CalculateTouchZone();
                Events.Instance.restartTimer?.Invoke();
                //Super effect
            }
            else
            {
                Events.Instance.LoseEvent.Invoke();
            }
        }

        if (localClickCounter == GameController.Instance.clickCounter)
        {
            Events.Instance.WinEvent.Invoke();
        }


    }

    private void StartLoop()
    {
        gameObject.GetComponent<Slider>().value = 0;
        tween = gameObject.GetComponent<Slider>().DOValue(gameObject.GetComponent<Slider>().maxValue, timeToReach).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetAutoKill(false);
    }

    private void StopLoop()
    {
        tween.Kill();
        tween = gameObject.GetComponent<Slider>().DOValue(0, .3f).OnComplete(StartLoop);
    }

    private void MoveZone()
    {
        zonePosition = Random.Range(-possibleRandomRange, possibleRandomRange);
        zonesFolder.DOLocalMoveY(zonePosition, .2f);
        CalculateTouchZone();
    }

    private void CalculateTouchZone()
    {
        centerZonePosition = zonePosition + halfSliderHeight;

        greenRangePos = (centerZonePosition + halfZoneHeight) / sliderHeight;
        greenRangeNeg = (centerZonePosition - Mathf.Abs(blueZone.rect.height - greenZone.rect.height) / 2) / sliderHeight;

        blueRangePos = greenRangeNeg;
        blueRangeNeg = greenRangeNeg - blueZone.rect.height / sliderHeight;
    }
}
