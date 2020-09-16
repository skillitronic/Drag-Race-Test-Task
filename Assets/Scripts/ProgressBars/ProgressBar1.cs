using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ProgressBar1 : MonoBehaviour
{
    [SerializeField] private float timeToReach;

    [SerializeField] private RectTransform zonesFolder;
    [SerializeField] private RectTransform greenZone;
    [SerializeField] private RectTransform blueZone;

    [SerializeField] private Slider slider;

    private Tween tween;

    private float sliderHeight;
    private float zonePosition;
    private float centerZonePosition;
    private float possibleRandomRange;
    private float greenRangeTop;
    private float greenRangeBot;
    private float blueRangeTop;
    private float blueRangeBot;

    private int localClickCounter;


    private void Awake()
    {
        localClickCounter = GameController.Instance.clicksToWin;

        sliderHeight = GetComponent<RectTransform>().rect.height;

        possibleRandomRange = sliderHeight / 2 - zonesFolder.rect.height / 2;
        zonePosition = Random.Range(-possibleRandomRange, possibleRandomRange);
        zonesFolder.localPosition = new Vector2(0, zonePosition);

        CalculateTouchZone();

        StartLoop();
    }

    private void Start()
    {
        Events.Instance.ZoneClickEvent += () => localClickCounter--;
        Events.Instance.ZoneClickEvent += RestartLoop;
        Events.Instance.ZoneClickEvent += MoveZone;
        Events.Instance.ZoneClickEvent += CalculateTouchZone;
        Events.Instance.ZoneClickEvent += CameraScript.Instance.ChangeCameraFOVAnimation;

        Events.Instance.GreenZoneClickEvent += Events.Instance.ZoneClickEvent.Invoke;
        Events.Instance.BlueZoneClickEvent += Events.Instance.ZoneClickEvent.Invoke;
    }

    private void OnDisable()
    {
        Events.Instance.ZoneClickEvent -= () => localClickCounter--;
        Events.Instance.ZoneClickEvent -= RestartLoop;
        Events.Instance.ZoneClickEvent -= MoveZone;
        Events.Instance.ZoneClickEvent -= CalculateTouchZone;
        Events.Instance.ZoneClickEvent -= CameraScript.Instance.ChangeCameraFOVAnimation;

        Events.Instance.GreenZoneClickEvent -= Events.Instance.ZoneClickEvent.Invoke;
        Events.Instance.BlueZoneClickEvent -= Events.Instance.ZoneClickEvent.Invoke;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (slider.value >= greenRangeBot && slider.value <= greenRangeTop)
            {
                Events.Instance.GreenZoneClickEvent?.Invoke();
            }
            else if (slider.value >= blueRangeBot && slider.value <= blueRangeTop)
            {
                Events.Instance.BlueZoneClickEvent?.Invoke();
            }
            else
            {
                Events.Instance.LoseEvent.Invoke();
            }
        }

        if (localClickCounter == 0)
        {
            Events.Instance.WinEvent.Invoke();
        }
    }

    private void StartLoop()
    {
        slider.value = 0;

        tween = slider.DOValue(slider.maxValue, timeToReach).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void RestartLoop()
    {
        tween.Kill();

        tween = slider.DOValue(0, .3f).OnComplete(StartLoop);
    }

    private void MoveZone()
    {
        zonePosition = Random.Range(-possibleRandomRange, possibleRandomRange);

        zonesFolder.DOLocalMoveY(zonePosition, .2f);

        CalculateTouchZone();
    }

    private void CalculateTouchZone()
    {
        centerZonePosition = zonePosition + sliderHeight / 2;

        greenRangeTop = (centerZonePosition + zonesFolder.rect.height / 2) / sliderHeight;
        greenRangeBot = (centerZonePosition - Mathf.Abs(blueZone.rect.height - greenZone.rect.height) / 2) / sliderHeight;

        blueRangeTop = greenRangeBot;
        blueRangeBot = greenRangeBot - blueZone.rect.height / sliderHeight;
    }
}
