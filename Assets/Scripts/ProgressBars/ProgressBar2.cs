using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    [SerializeField] private Slider sliders;

    [SerializeField] private Image fillImage;
    [SerializeField] private Color32 greenColor;
    [SerializeField] private Color32 redColor;

    [SerializeField] private float maxTimer;

    private float timer;

    private void Start()
    {
        Events.Instance.ZoneClickEvent += () => sliders.value = 0;
    }

    private void OnDisable()
    {
        Events.Instance.ZoneClickEvent -= () => sliders.value = 0;
    }

    private void Update()
    {


        if (sliders.value == 0)
        {
            timer = 0;
            fillImage.color = greenColor;
        }

        if (sliders.value < sliders.maxValue)
        {
            sliders.value += Time.deltaTime / 10; // 10 seconds before starts lose timer
        }
        else if (sliders.value == sliders.maxValue)
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
