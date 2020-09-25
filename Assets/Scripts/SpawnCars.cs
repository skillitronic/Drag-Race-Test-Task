using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField] private GameObject carReference = null;
    private GameObject carBumper;
    public void OnEnable()
    {
        if (carReference == null)
        {
            if (GameController.Instance.carContainer.cars[GameController.Instance.carContainer.carIndex].GetComponent<PlayerCar>())
            {
                carBumper = Instantiate(GameController.Instance.carContainer.cars[GameController.Instance.carContainer.carIndex], GameController.Instance.instantiater.transform);
                GameController.Instance.carReference = carBumper;
                CameraScript.Instance.playerCar = carBumper.transform;
            }
        }
        else
        {
            carBumper = Instantiate(carReference, GameController.Instance.instantiater.transform);
        }

        carBumper.transform.position = transform.position;

    }

    public void OnDisable()
    {
        carBumper = null;
    }
}
