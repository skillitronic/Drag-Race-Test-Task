using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    public float maxValue;
    private Slider slider;

    [SerializeField] private float maxTimer;
    private float timer;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = maxValue;
    }

    void Update()
    {
        if (slider.value == 0)
        {
            timer = 0;
        }

        if (slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime;
        }
        else if (slider.value == slider.maxValue)
        {
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
