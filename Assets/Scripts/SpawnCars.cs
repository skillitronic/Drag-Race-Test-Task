using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    public GameObject carReference;
    public void OnEnable()
    {
        GameObject car = Instantiate(carReference, GameController.Instance.instantiater.transform);
        car.transform.position = transform.position;
    }
}
