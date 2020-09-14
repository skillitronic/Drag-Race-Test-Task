using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar1 : MonoBehaviour
{
    private Slider slider;
    public Transform ZonesFolder;
    public float timeToReach;
    private Tween tween;

    public int check;



    private void Awake()
    {
        slider = GetComponent<Slider>();
        check = Random.Range(0, 240);
        StartLoop();
        ZonesFolder.localPosition = Vector2.up * check;
    }

    private void Start()
    {

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopLoop();

        }
    }

    private void StartLoop()
    {
        tween = slider.DOValue(slider.maxValue, timeToReach).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetAutoKill(false);
        tween.OnComplete(() => tween.Restart());
    }

    private void StopLoop()
    {
        tween.Kill();
        tween = slider.DOValue(0, .5f).OnComplete(StartLoop);
    }
}
