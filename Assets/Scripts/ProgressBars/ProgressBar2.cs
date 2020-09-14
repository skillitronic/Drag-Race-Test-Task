using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    public float maxValue;
    private Slider slider;

    [SerializeField] private float maxTimer;
    private float timer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Events.Instance.LoseEvent.AddListener(() => timer = 0);
        Events.Instance.restartTimer += (() => slider.DOValue(0, .1f));
    }

    private void OnDisable()
    {
        Events.Instance.LoseEvent.RemoveListener(() => timer = 0);
        Events.Instance.restartTimer -= (() => slider.DOValue(0, .1f));
    }

    void Start()
    {
        slider.maxValue = maxValue;
    }

    void Update()
    {
        if (slider.value == 0)
        {
            timer = 0;
            slider.fillRect.GetComponent<Image>().color = Color.green;
        }

        if (slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime;
        }
        else if (slider.value == slider.maxValue)
        {
            slider.fillRect.GetComponent<Image>().color = Color.red;
            if (timer < maxTimer)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= maxTimer)
            {
                Events.Instance.LoseEvent.Invoke();
            }
        }
    }
}
