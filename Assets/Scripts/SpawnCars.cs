using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField] private GameObject carReference;
    public void Awake()
    {
        GameObject car = Instantiate(carReference, GameController.Instance.instantiater.transform);
        car.transform.position = transform.position;
    }
}
