using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]protected bool hasCrossed = false;

    private void Awake()
    {
        hasCrossed = false;
    }

    private void Start()
    {
        GameController.Instance.finishLineReference = transform;
    }

    private void OnDestroy()
    {
        hasCrossed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasCrossed == false)
        {
            if (other.gameObject.GetComponent<PlayerCar>())
            {
                Events.Instance.WinEvent.Invoke();
            }
            else if (other.gameObject.GetComponent<EnemyCar>())
            {
                Events.Instance.LoseEvent.Invoke();
            }
            hasCrossed = true;
        }
        
    }
}
