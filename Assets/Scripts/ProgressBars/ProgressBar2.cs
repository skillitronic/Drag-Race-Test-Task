using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    [SerializeField] private Image fillImage = null;
    [SerializeField] private Color32 greenColor = Color.green;
    [SerializeField] private Color32 redColor = Color.red;

    [SerializeField] private float maxTimer = 0;

    public float timer;

    private void Awake()
    {
        Events.Instance.GreenZoneClickEvent += () => slider.value = 0;
        Events.Instance.BlueZoneClickEvent += () => slider.value = 0;
        Events.Instance.WinEvent.AddListener(() => slider.value = 0);
    }

    private void OnDisable()
    {
/*        Events.Instance.GreenZoneClickEvent -= () => slider.value = 0;
        Events.Instance.BlueZoneClickEvent -= () => slider.value = 0;
        Events.Instance.WinEvent.RemoveListener(() => slider.value = 0);*/
        slider.value = 0;
    }

    private void Update()
    {

        if (slider.value == 0)
        {
            timer = 0;
            fillImage.color = greenColor;
        }

        if (slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime / 5; // 5 seconds before starts lose timer
        }
        else if (slider.value == slider.maxValue)
        {
            fillImage.color = redColor;
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
