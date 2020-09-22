using UnityEngine;
using UnityEngine.UI;

public class DistanceProgressSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Transform playerCar;
    private Transform finishLine;

    

    private void OnEnable()
    {
        playerCar = GameController.Instance.carReference;
        finishLine = GameController.Instance.finishLineReference;
    }

    private void OnDisable()
    {
        playerCar = null;
        finishLine = null;
    }


    private void Update()
    {
        slider.value = playerCar.position.z / finishLine.position.z;
    }
}
