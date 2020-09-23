using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField] private GameObject carReference = null;
    private GameObject carBumper;
    public void OnEnable()
    {
        carBumper = Instantiate(carReference, GameController.Instance.instantiater.transform);
        carBumper.transform.position = transform.position;
        if (carBumper.GetComponent<PlayerCar>())
        {
            GameController.Instance.carReference = carBumper;
            CameraScript.Instance.playerCar = carBumper.transform;

        }
    }

    public void OnDisable()
    {
        carBumper = null;
    }
}
