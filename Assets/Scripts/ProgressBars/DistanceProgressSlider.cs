using UnityEngine;
using UnityEngine.UI;

public class DistanceProgressSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Transform playerCar;
    public Transform finishLine;

    private void OnEnable()
    {
        playerCar = GameController.Instance.carReference.transform;
        finishLine = GameController.Instance.finishLineReference;
    }

    private void OnDisable()
    {
        playerCar = null;
        finishLine = null;
        slider.value = 0;
    }


    private void Update()
    {
        slider.value = playerCar.position.z / finishLine.position.z;
    }
}
